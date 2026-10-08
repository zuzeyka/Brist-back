using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Slush.Tests
{
    // Program.cs reads ConnectionStrings:MySqlDb into a local variable before
    // builder.Build() runs, so a WebHostBuilder.ConfigureAppConfiguration override
    // (which only takes effect once the host is built) arrives too late to change it.
    // The env-var config provider, by contrast, is already wired up when
    // WebApplication.CreateBuilder(args) runs at the top of Program.cs, so setting the
    // variable before the host factory ever executes Program.cs is what actually works.
    // This points the app at a throwaway MySQL schema instead of the real BristDB, so
    // integration tests never touch data a human is looking at.
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        static CustomWebApplicationFactory()
        {
            Environment.SetEnvironmentVariable(
                "ConnectionStrings__MySqlDb",
                "Server=localhost;Port=3306;Database=BristTestDB;Uid=root;Pwd=devpassword;AllowPublicKeyRetrieval=True;SslMode=None;");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
        }
    }
}
