using FullStackBrist.Server.Models.Profile;
using Slush.DAO.ProfileDao;
using Slush.Data.Entity.Profile;
using Slush.Services.Hash;

namespace Slush.Services.RegistrationValidation
{
    public class RegistrationService
    {
        private readonly HashPasswordService _passwordService;
        private readonly UserDao _userDao;

        public RegistrationService(HashPasswordService passwordService, UserDao userDao)
        {
            _passwordService = passwordService;
            _userDao = userDao;
        }

        public async Task Registration(UserModel model)
        {
            var hashedPassword = _passwordService.Generate(model.passwordSalt);

            var result = _userDao.Add(new User(
                Guid.NewGuid(),
                model.name,
                hashedPassword,
                model.email,
                model.phone,
                false,
                DateTime.Now
                ));
        }

        public async Task<String> Login(UserModel model)
        {
            return "";
        }
    }
}
