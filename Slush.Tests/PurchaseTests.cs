using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Slush.Data;
using Slush.Data.Entity.Profile;
using Xunit;

namespace Slush.Tests
{
    // Regression coverage for the new /api/Purchase/checkout endpoint: the only path
    // in the app that actually grants ownership and moves money.
    public class PurchaseTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private Guid _seededGameId;
        private float _seededGameFinalPrice;
        private Guid _seededDlcId;

        public PurchaseTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        public async Task InitializeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            // Must actually cost something — the insufficient-balance test depends
            // on that, and FirstAsync() alone isn't guaranteed to land on a priced
            // item if the seed data ever includes a free one.
            var game = await context.dbGamesInShops.Where(g => g.price > 0).FirstAsync();
            _seededGameId = game.id;
            _seededGameFinalPrice = FinalPrice(game.price, game.discount);
            _seededDlcId = await context.dbDLCsInShop.Select(d => d.id).FirstAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<Guid> SeedUserAsync(float amountOfMoney)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var user = new User(Guid.NewGuid(), "test", "x", null, null, null, true, amountOfMoney, 0, DateTime.UtcNow);
            context.dbUsers.Add(user);
            await context.SaveChangesAsync();
            return user.id;
        }

        [Fact]
        public async Task Checkout_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsJsonAsync("/api/Purchase/checkout", new[]
            {
                new { itemId = _seededGameId, itemType = "game" },
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Checkout_Game_GrantsOwnershipAndDeductsBalance()
        {
            var userId = await SeedUserAsync(_seededGameFinalPrice + 100);
            var client = _factory.AuthenticatedClient(userId);

            var response = await client.PostAsJsonAsync("/api/Purchase/checkout", new[]
            {
                new { itemId = _seededGameId, itemType = "game" },
            });

            var body = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode, body);
            var result = await response.Content.ReadFromJsonAsync<CheckoutResultDto>();
            Assert.Equal(_seededGameId, result!.purchasedItemIds.Single());
            Assert.Equal(100, result.newBalance);

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var owned = await context.dbOwnedGames.FirstOrDefaultAsync(g => g.userId == userId && g.ownedGameId == _seededGameId);
            Assert.NotNull(owned);
            var user = await context.dbUsers.FindAsync(userId);
            Assert.Equal(100, user!.amountOfMoney);
        }

        [Fact]
        public async Task Checkout_Dlc_GrantsOwnership()
        {
            using var seedScope = _factory.Services.CreateScope();
            var seedContext = seedScope.ServiceProvider.GetRequiredService<DataContext>();
            var dlcPrice = (await seedContext.dbDLCsInShop.FindAsync(_seededDlcId))!.price;

            var userId = await SeedUserAsync(dlcPrice + 50);
            var client = _factory.AuthenticatedClient(userId);

            var response = await client.PostAsJsonAsync("/api/Purchase/checkout", new[]
            {
                new { itemId = _seededDlcId, itemType = "dlc" },
            });

            response.EnsureSuccessStatusCode();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var owned = await context.dbOwnedDlcs.FirstOrDefaultAsync(d => d.userId == userId && d.ownedDlcId == _seededDlcId);
            Assert.NotNull(owned);
        }

        [Fact]
        public async Task Checkout_InsufficientBalance_Returns402_AndGrantsNothing()
        {
            var userId = await SeedUserAsync(0);
            var client = _factory.AuthenticatedClient(userId);

            var response = await client.PostAsJsonAsync("/api/Purchase/checkout", new[]
            {
                new { itemId = _seededGameId, itemType = "game" },
            });

            Assert.Equal(HttpStatusCode.PaymentRequired, response.StatusCode);

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var owned = await context.dbOwnedGames.FirstOrDefaultAsync(g => g.userId == userId && g.ownedGameId == _seededGameId);
            Assert.Null(owned);
        }

        [Fact]
        public async Task Checkout_AlreadyOwnedItem_Returns409_AndDoesNotDoubleCharge()
        {
            var userId = await SeedUserAsync(_seededGameFinalPrice * 3);
            var client = _factory.AuthenticatedClient(userId);

            var first = await client.PostAsJsonAsync("/api/Purchase/checkout", new[]
            {
                new { itemId = _seededGameId, itemType = "game" },
            });
            first.EnsureSuccessStatusCode();

            var second = await client.PostAsJsonAsync("/api/Purchase/checkout", new[]
            {
                new { itemId = _seededGameId, itemType = "game" },
            });

            Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var user = await context.dbUsers.FindAsync(userId);
            // Only the first, successful purchase should have been charged.
            Assert.Equal(_seededGameFinalPrice * 3 - _seededGameFinalPrice, user!.amountOfMoney);
        }

        [Fact]
        public async Task Checkout_IgnoresClientSuppliedPrice()
        {
            // The request model has no price field at all, but assert the server-side
            // price actually matches the catalog rather than trusting whatever a client
            // might smuggle in under an unrelated/extra JSON property.
            var userId = await SeedUserAsync(_seededGameFinalPrice);
            var client = _factory.AuthenticatedClient(userId);

            var response = await client.PostAsJsonAsync("/api/Purchase/checkout", new[]
            {
                new { itemId = _seededGameId, itemType = "game", price = 0 },
            });

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CheckoutResultDto>();
            Assert.Equal(0, result!.newBalance);
        }

        private static float FinalPrice(float price, int discount)
        {
            return MathF.Floor(price - price * discount / 100f);
        }

        private record CheckoutResultDto(float newBalance, List<Guid> purchasedItemIds);
    }
}
