using FullStackBrist.Server.Models.Profile;
using Slush.DAO.ProfileDao;
using Slush.Data.Entity.Profile;
using Slush.Services.Hash;
using Slush.Services.Minio;

namespace Slush.Services.RegistrationValidation
{
    public class RegistrationService
    {
        private readonly HashPasswordService _passwordService;
        private readonly MinioService _minioService;
        private readonly UserDao _userDao;

        public RegistrationService(HashPasswordService passwordService, UserDao userDao, MinioService minioService)
        {
            _passwordService = passwordService;
            _userDao = userDao;
            _minioService = minioService;
        }

        public async Task<User> Registration(UserModel model)
        {
            var hashedPassword = _passwordService.Generate(model.passwordSalt);

            var user = await _userDao.GetByEmail(model.name);

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
                await _userDao.Add(result);

                return result;
            }

            return null;
        }
    }
}
