using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[userController]")]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserDao _userDao;

        public UserController(DataContext dataContext, UserDao userDao)
        {
            _dataContext = dataContext;
            _userDao = userDao;
        }

        public async Task<ActionResult<List<UserDao>>> GetAllUsers()
        {
            var _users = await _userDao.GetAllUsers();

            var response = _users.Select(u => new User(u.id,
                                                            u.name,
                                                            u.passwordSalt,
                                                            u.salt,
                                                            u.email,
                                                            u.phone)).ToList();

            return Ok(response);
        }
    }
}
