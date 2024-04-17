using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            var response = _users.Select(u => new User(id: u.id,
                                                       name: u.name,
                                                       passwordSalt: u.passwordSalt,
                                                       salt: u.salt,
                                                       email: u.email,
                                                       phone: u.phone,
                                                       createdAt: u.createdAt)).ToList();

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserModel model)
        {
            var result = new User(Guid.NewGuid(),
                model.name,
                model.passwordSalt,
                model.salt,
                model.email,
                model.phone,
                DateTime.Now);

            var response = _dataContext.dbUsers.AddAsync(result);

            return Ok(response);
        }
    }
}
