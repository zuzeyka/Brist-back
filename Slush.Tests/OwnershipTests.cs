using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Slush.Data;
using Slush.Data.Entity.Profile;
using Xunit;

namespace Slush.Tests
{
    // Regression coverage for the ownership-check pass over WishedGame, Screenshot,
    // Video, Topic and UserComment — previously anyone (even logged out, for
    // WishedGame) could read/write any other user's data through these controllers.
    public class OwnershipTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private Guid _seededGameId;

        public OwnershipTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        public async Task InitializeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            // WishedGame.ownedGameId has a real FK to GameInShop — DbSeeder already
            // populated the test DB with real games on first host startup, so reuse one
            // instead of inserting a throwaway row with a different FK shape.
            _seededGameId = await context.dbGamesInShops.Select(g => g.id).FirstAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<Guid> SeedUserAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var user = new User(Guid.NewGuid(), "test", "x", null, null, null, true, 0, 0, DateTime.UtcNow);
            context.dbUsers.Add(user);
            await context.SaveChangesAsync();
            return user.id;
        }

        [Fact]
        public async Task WishedGame_GetAll_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/WishedGame");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task WishedGame_Create_IgnoresSpoofedUserId()
        {
            var realUserId = await SeedUserAsync();
            var spoofedUserId = await SeedUserAsync();
            var client = _factory.AuthenticatedClient(realUserId);

            var response = await client.PostAsJsonAsync("/api/WishedGame", new
            {
                ownedGameId = _seededGameId,
                userId = spoofedUserId,
            });

            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<WishedGameDto>();
            Assert.Equal(realUserId, created!.userId);
        }

        [Fact]
        public async Task WishedGame_Delete_ByNonOwner_Returns403()
        {
            var ownerId = await SeedUserAsync();
            var ownerClient = _factory.AuthenticatedClient(ownerId);
            var created = await (await ownerClient.PostAsJsonAsync("/api/WishedGame", new
            {
                ownedGameId = _seededGameId,
                userId = ownerId,
            })).Content.ReadFromJsonAsync<WishedGameDto>();

            var attackerId = await SeedUserAsync();
            var attackerClient = _factory.AuthenticatedClient(attackerId);
            var response = await attackerClient.DeleteAsync($"/api/WishedGame/{created!.id}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Screenshot_Get_StaysPublic_LoggedOut()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Screenshot");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Screenshot_Create_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsJsonAsync("/api/Screenshot", new
            {
                title = "x",
                gameId = _seededGameId,
                authorId = Guid.NewGuid(),
                contentUrl = "x",
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Screenshot_Create_IgnoresSpoofedAuthorId()
        {
            var realUserId = await SeedUserAsync();
            var spoofedAuthorId = await SeedUserAsync();
            var client = _factory.AuthenticatedClient(realUserId);

            var response = await client.PostAsJsonAsync("/api/Screenshot", new
            {
                title = "x",
                gameId = _seededGameId,
                authorId = spoofedAuthorId,
                contentUrl = "x",
            });

            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<ScreenshotDto>();
            Assert.Equal(realUserId, created!.authorId);
        }

        private record WishedGameDto(Guid id, Guid ownedGameId, Guid userId, DateTime? createdAt);
        private record ScreenshotDto(Guid id, string? title, Guid gameId, Guid authorId, string? contentUrl);
    }
}
