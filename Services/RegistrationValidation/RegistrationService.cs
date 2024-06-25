using FullStackBrist.Server.Models.Profile;
using Slush.Repositories.ProfileRepository;
using Slush.Data.Entity.Profile;
using Slush.Services.Hash;
using Slush.Services.Minio;

namespace Slush.Services.RegistrationValidation
{
    public class RegistrationService
    {
        private readonly HashPasswordService _passwordService;
        private readonly MinioService _minioService;
        private readonly UserRepository _userRepositories;

        public RegistrationService(HashPasswordService passwordService, UserRepository userRepositories, MinioService minioService)
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
