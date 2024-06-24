using FullStackBrist.Server.Models.Profile;
using FullStackBrist.Server.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data.Entity.Profile;
using Slush.Models.Validation;
using Slush.Services.Hash;
using Slush.Services.JWT;
using Slush.Services.Minio;
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
        private readonly MinioService _minioService;
        private readonly EmailService _emailService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserDao userDao, RegistrationService registrationService, HashPasswordService passwordService, JWTService jwtService, ILogger<UserController> logger, MinioService minioService, EmailService emailService)
        {
            _userDao = userDao;
            _registrationService = registrationService;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _logger = logger;
            _minioService = minioService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDao>>> GetAllUsers()
        {
            var _users = await _userDao.GetAllUsers();

            return Ok(_users);
        }

        #region Registration

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserModel model)
        {

            var res = await _registrationService.Registration(model);

            if(res == null)
            {
                return BadRequest("User already exist");
            }

            var code = await _emailService.SendEmail(model.email);

            var token = _jwtService.GenerateToken(res);

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
                resToken = token,
                user = res
            };

            return Ok(result);
        }

        [HttpPost("resend")]
        public async Task<ActionResult<String>> ResendEmailCode([FromBody] UserModel model)
        {
            var user = await _userDao.GetByEmail(model.email);
            if (user != null) 
            {
                var code = await _emailService.SendEmail(model.email);

                return code;
            }

            return "User is null";
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


            _logger.LogWarning("Login token: {1}", result);

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
        [HttpGet("getbyuid/{id}")]
        public async Task<ActionResult<User>> GetUserByUid(Guid id)
        {
            var response = await _userDao.GetByUserId(id);
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
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserModel user, IFormFile file)
        {
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        user.image = url;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex.Message);
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }
            var result = await _userDao.UpdateUser(new User(id, user.name, user.passwordSalt, user.email, user.description, user.image, user.verified, user.amountOfMoney, user.amountOfXp, user.createdAt));
            return Ok(result);
        }
        [HttpPost("getall")]
        public async Task<ActionResult<List<User>>> GetAllUsersByIds([FromBody] List<Guid> guidList)
        {
            var response = await _userDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
