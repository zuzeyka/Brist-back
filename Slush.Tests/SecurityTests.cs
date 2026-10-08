using System.Net;
using Xunit;

namespace Slush.Tests
{
    // Regression coverage for the critical data-leak fixes: UserCategoryController's
    // duplicate unauthenticated "everyone's owned games" endpoint and
    // SettingsController's unauthenticated "everyone's notification prefs" endpoint.
    public class SecurityTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public SecurityTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Settings_GetByUserId_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Settings/getbyuid/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Settings_GetByUserId_AsSelf_IsNotForbidden()
        {
            var userId = Guid.NewGuid();
            var client = _factory.AuthenticatedClient(userId);

            var response = await client.GetAsync($"/api/Settings/getbyuid/{userId}");

            // No settings row exists for this fresh user yet, but the ownership check
            // itself must not be what rejects the caller.
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Settings_GetByUserId_AsDifferentUser_Returns403()
        {
            var client = _factory.AuthenticatedClient(Guid.NewGuid());

            var response = await client.GetAsync($"/api/Settings/getbyuid/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Settings_BareGetAll_NoLongerExists()
        {
            var client = _factory.AuthenticatedClient(Guid.NewGuid());

            var response = await client.GetAsync("/api/Settings");

            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UserCategory_GetByUserId_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/UserCategory/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task UserCategory_GetByUserId_AsSelf_Returns200()
        {
            var userId = Guid.NewGuid();
            var client = _factory.AuthenticatedClient(userId);

            var response = await client.GetAsync($"/api/UserCategory/{userId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UserCategory_GetByUserId_AsDifferentUser_Returns403()
        {
            var client = _factory.AuthenticatedClient(Guid.NewGuid());

            var response = await client.GetAsync($"/api/UserCategory/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task UserCategory_OldLeakRoutes_NoLongerServeData()
        {
            var client = _factory.AuthenticatedClient(Guid.NewGuid());

            var ownedGames = await client.GetAsync("/api/UserCategory/getownedgames");
            var categoriesByUser = await client.GetAsync("/api/UserCategory/getcategoriesbyuser");

            Assert.NotEqual(HttpStatusCode.OK, ownedGames.StatusCode);
            Assert.NotEqual(HttpStatusCode.OK, categoriesByUser.StatusCode);
        }
    }
}
