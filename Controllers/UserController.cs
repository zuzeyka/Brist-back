using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity.Profile;
using Slush.Models.Validation;
using Slush.Services.Hash;
using Slush.Services.JWT;
using Slush.Services.FileStorage;
using Slush.Services.RegistrationValidation;
using Slush.Repositories.IRepository;
using Slush.Services.Email;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepositories;
        private readonly IRegistrationService _registrationService;
        private readonly IHashPasswordService _passwordService;
        private readonly IJWTService _jwtService;
        private readonly IFileStorageService _fileStorageService;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepositories, IRegistrationService registrationService, IHashPasswordService passwordService, IJWTService jwtService, ILogger<UserController> logger, IFileStorageService fileStorageService, IEmailService emailService)
        {
            _userRepositories = userRepositories;
            _registrationService = registrationService;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _logger = logger;
            _fileStorageService = fileStorageService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IUserRepository>>> GetAllUsers()
        {
            var _users = await _userRepositories.GetAllUsers();

            return Ok(_users);
        }

        #region Registration

        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult<String>> ResendEmailCode([FromBody] UserModel model)
        {
            var user = await _userRepositories.GetByEmail(model.email);
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
        [AllowAnonymous]
        public async Task<IActionResult> LoginByModel([FromBody] LoginValidationModel model)
        {
            string token;
            try
            {
                token = await Login(model);
            }
            catch (Exception)
            {
                return Unauthorized("Invalid username/email or password");
            }

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
            var user = await _userRepositories.GetByEmail(validationModel.username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

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
            var response = await _userRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        [HttpGet("getbyuid/{id}")]
        public async Task<ActionResult<User>> GetUserByUid(Guid id)
        {
            var response = await _userRepositories.GetByUserId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            if (!IsCurrentUser(id))
            {
                return Forbid();
            }

            await _userRepositories.DeleteUser(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserModel user, IFormFile file)
        {
            if (!IsCurrentUser(id))
            {
                return Forbid();
            }

            var existing = await _userRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }

            if (file != null && file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _fileStorageService.SaveFile("images", id, file.FileName, stream);

                        var url = await _fileStorageService.GetUrlToFile(imageUrl);

                        user.image = url;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex.Message);
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            // Only profile fields are client-editable here; password, verification
            // status and balances must never be settable through this endpoint.
            var result = await _userRepositories.UpdateUser(new User(
                id,
                user.name,
                existing.passwordSalt,
                user.email,
                user.description,
                user.image,
                existing.verified,
                existing.amountOfMoney,
                existing.amountOfXp,
                existing.createdAt));
            return Ok(result);
        }

        private bool IsCurrentUser(Guid id)
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) && currentUserId == id;
        }
        [HttpPost("getall")]
        public async Task<ActionResult<List<User>>> GetAllUsersByIds([FromBody] List<Guid> guidList)
        {
            var response = await _userRepositories.GetByIds(guidList);
             
            return Ok(response);
        }
    }
}
