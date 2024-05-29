using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Profile;
using Slush.Models.Validation;
using Slush.Services.Hash;
using Slush.Services.JWT;
using Slush.Services.RegistrationValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserDao _userDao;
        private readonly RegistrationService _registrationService;
        private readonly HashPasswordService _passwordService;
        private readonly JWTService _jwtService;
        private readonly ILogger<UserController> _logger;

        public UserController(DataContext dataContext, UserDao userDao, RegistrationService registrationService, HashPasswordService passwordService, JWTService jwtService, ILogger<UserController> logger)
        {
            _dataContext = dataContext;
            _userDao = userDao;
            _registrationService = registrationService;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDao>>> GetAllUsers()
        {
            var _users = await _userDao.GetAllUsers();

            var response = _users.Select(u => new User(id: u.id,
                                                       name: u.name,
                                                       passwordSalt: u.passwordSalt,
                                                       email: u.email,
                                                       phone: u.phone,
                                                       verified: u.verified,
                                                       createdAt: u.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserModel model)
        {
            await _registrationService.Registration(model);

            return Ok();
        }

        #region Login

        [HttpPost("validatelogin")]
        public async Task<IActionResult> LoginByModel([FromBody] LoginValidationModel model)
        {
            _logger.LogInformation("HttpContext is null: {0}", HttpContext == null);
            var token = await Login(model);

            var httpContext = HttpContext;
            httpContext.Response.Cookies.Append("somedonuts", token);

            var result = new
            {
                res = token,
            };

            return Ok(result);
        }

        private async Task<string> Login(LoginValidationModel validationModel)
        {
            var user = await _userDao.GetByEmail(validationModel.email);

            var result = _passwordService.Verify(validationModel.password, user.passwordSalt);

            if (!result)
            {
                throw new Exception("Failed to login in controller");
            }

            var token = _jwtService.GenerateToken(user);

            return token;
        }
        #endregion

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var response = await _userDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userDao.DeleteUser(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserModel user)
        {
            var result = new User(id, user.name, user.passwordSalt, user.email, user.phone, user.verified, user.createdAt);
            await _userDao.UpdateUser(result);
            return NoContent();
        }
    }
}
