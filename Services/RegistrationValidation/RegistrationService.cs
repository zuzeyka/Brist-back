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

        public async Task Registration(UserModel model, IFormFile file)
        {
            var hashedPassword = _passwordService.Generate(model.passwordSalt);
            var result = new User(
                Guid.NewGuid(),
                model.name,
                hashedPassword,
                model.email,
                model.description,
                model.image,
                false,
                0,
                DateTime.Now
                );
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", result.id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        result.image = url;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            await _userDao.Add(result);
        }

        public async Task<String> Login(UserModel model)
        {
            return "";
        }
    }
}
