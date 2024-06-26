using FullStackBrist.Server.Models.Profile;
using Slush.Data.Entity.Profile;

namespace Slush.Services.RegistrationValidation
{
    public interface IRegistrationService
    {
        Task<User> Registration(UserModel model);
    }
}
