using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Profile;
using Slush.Models.Validation;
using Slush.Services.Hash;
using Slush.Services.JWT;
using Slush.Services.RegistrationValidation;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserDao _userDao;
        private readonly RegistrationService _registrationService;
        private readonly HashPasswordService _passwordService;
        private readonly JWTService _jwtService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserDao userDao, RegistrationService registrationService, HashPasswordService passwordService, JWTService jwtService, ILogger<UserController> logger)
        {
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

            return Ok(_users);
        }

        #region Registration

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserModel model)
        {
            await _registrationService.Registration(model);

            return Ok();
        }
        #endregion
        #region Login

        [HttpPost("validatelogin")]
        public async Task<IActionResult> LoginByModel([FromBody] LoginValidationModel model)
        {
            var token = await Login(model);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                IsEssential = true,
            };
            cookieOptions.Expires = DateTime.UtcNow.AddHours(1);
            Response.Cookies.Append("somedonuts", token, cookieOptions);

            var result = new
            {
                res = token,
            };

            return Ok(result);
        }

        private async Task<string> Login(LoginValidationModel validationModel)
        {
            var user = await _userDao.GetByEmail(validationModel.username);

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
            await _userDao.UpdateUser(new User(id, user.name, user.passwordSalt, user.email, user.phone, user.description, user.verified, user.createdAt));
            return NoContent();
        }
    }
}
