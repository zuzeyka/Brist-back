using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Slush.Data;
using Xunit;

namespace Slush.Tests
{
    // Regression coverage for the data-integrity fixes: GameInShop creation silently
    // no-oping (missing await), and Video creation never actually attaching an
    // uploaded file (contentUrl always null).
    public class DataIntegrityTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public DataIntegrityTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GameInShop_Add_ActuallyPersists()
        {
            // Goes through the repository directly rather than the HTTP controller:
            // GamesInShopController.CreateGameInShop mixes [FromBody] with an IFormFile
            // parameter (a separate, pre-existing 415 conflict out of scope here) — this
            // test is only about Add() itself no longer silently no-oping.
            using var scope = _factory.Services.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<Slush.Repositories.IRepository.IGameInShopRepository>();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var name = $"test-game-{Guid.NewGuid()}";

            var developerId = Guid.NewGuid();
            var publisherId = Guid.NewGuid();
            context.dbDevelopers.Add(new Slush.Entity.Store.Product.Creators.Developer(
                developerId, 0, "test-dev", "x", null, null, "x", DateTime.UtcNow));
            context.dbPublishers.Add(new Slush.Entity.Store.Product.Creators.Publisher(
                publisherId, 0, "test-pub", "x", null, null, "x", DateTime.UtcNow));
            await context.SaveChangesAsync();

            await repo.Add(new Slush.Entity.Store.Product.GameInShop(
                Guid.NewGuid(), name, 10, 0, null, "x", "x", DateTime.UtcNow, developerId, publisherId, "x", DateTime.UtcNow));

            var persisted = await context.dbGamesInShops.FirstOrDefaultAsync(g => g.name == name);
            Assert.NotNull(persisted);
        }

        [Fact]
        public async Task Video_Update_WithFile_SetsRealContentUrl()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var gameId = await context.dbGamesInShops.Select(g => g.id).FirstAsync();

            var userId = Guid.NewGuid();
            var user = new Slush.Data.Entity.Profile.User(userId, "test", "x", null, null, null, true, 0, 0, DateTime.UtcNow);
            context.dbUsers.Add(user);
            await context.SaveChangesAsync();

            var client = _factory.AuthenticatedClient(userId);
            var created = await (await client.PostAsJsonAsync("/api/Video", new
            {
                title = "x",
                likesCount = 0,
                gameId,
                authorId = userId,
                contentUrl = (string?)null,
            })).Content.ReadFromJsonAsync<VideoDto>();

            Assert.Null(created!.contentUrl);

            using var form = new MultipartFormDataContent
            {
                { new StringContent("x"), "title" },
                { new StringContent(gameId.ToString()), "gameId" },
                { new StringContent(userId.ToString()), "authorId" },
                { new StringContent("0"), "likesCount" },
            };
            var fileContent = new ByteArrayContent(new byte[] { 1, 2, 3, 4 });
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("video/mp4");
            form.Add(fileContent, "file", "test.mp4");

            var updateResponse = await client.PutAsync($"/api/Video/{created.id}", form);

            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
            var updated = await updateResponse.Content.ReadFromJsonAsync<VideoDto>();
            Assert.False(string.IsNullOrEmpty(updated!.contentUrl));
        }

        private record VideoDto(Guid id, string? title, Guid gameId, Guid authorId, string? contentUrl);
    }
}
