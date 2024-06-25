using Microsoft.AspNetCore.Mvc;
using FullStackBrist.Server.Services.Email;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public EmailController(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("send/{post}")]
        public async Task<ActionResult> SendEmail(String post)
        {
            var result = await _emailService.SendEmail(post);

            return Ok(result);
        }
    }
}
