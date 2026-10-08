using System.Net;
using Xunit;

namespace Slush.Tests
{
    public class SmokeTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public SmokeTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetGamesInShop_ReturnsOk()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/GamesInShop");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
