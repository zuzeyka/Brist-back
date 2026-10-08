using Microsoft.Extensions.DependencyInjection;
using Slush.Data.Entity.Profile;
using Slush.Services.JWT;

namespace Slush.Tests
{
    public static class AuthHelper
    {
        // Mints a real, correctly-signed JWT the same way UserController's login
        // endpoint does, without going through an actual HTTP login round-trip.
        public static string TokenFor(CustomWebApplicationFactory factory, Guid userId)
        {
            using var scope = factory.Services.CreateScope();
            var jwtService = scope.ServiceProvider.GetRequiredService<IJWTService>();
            return jwtService.GenerateToken(new User { id = userId });
        }

        public static HttpClient AuthenticatedClient(this CustomWebApplicationFactory factory, Guid userId)
        {
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Add("Cookie", $"somedonuts={TokenFor(factory, userId)}");
            return client;
        }
    }
}
