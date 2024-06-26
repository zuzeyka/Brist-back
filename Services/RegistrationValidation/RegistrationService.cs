using FullStackBrist.Server.Models.Profile;
using Slush.Data.Entity.Profile;
using Slush.Services.Hash;
using Slush.Services.Minio;
using Slush.Repositories.IRepository;

namespace Slush.Services.RegistrationValidation
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IHashPasswordService _passwordService;
        private readonly IMinioService _minioService;
        private readonly IUserRepository _userRepositories;

        public RegistrationService(IHashPasswordService passwordService, IUserRepository userRepositories, IMinioService minioService)
        {
            _passwordService = passwordService;
            _userRepositories = userRepositories;
            _minioService = minioService;
        }

        public async Task<User> Registration(UserModel model)
        {
            var hashedPassword = _passwordService.Generate(model.passwordSalt);

            var user = await _userRepositories.GetByEmail(model.name);

            if(user == null)
            {
                var result = new User(
                    Guid.NewGuid(),
                    model.name,
                    hashedPassword,
                    model.email,
                    model.description,
                    model.image,
                    false,
                    0,
                    0,
                    DateTime.Now
                    );
                await _userRepositories.Add(result);

                return result;
            }

            return null;
        }
    }
}
