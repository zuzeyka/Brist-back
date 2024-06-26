using Slush.Data.Entity.Profile;

namespace Slush.Services.JWT
{
    public interface IJWTService
    {
        String GenerateToken(User user);
    }
}
