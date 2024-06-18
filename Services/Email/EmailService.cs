using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using FullStackBrist.Server.Services.Random;

namespace FullStackBrist.Server.Services.Email
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly RandomService _randomService;

        public EmailService(IConfiguration configuration, RandomService randomService)
        {
            _configuration = configuration;
            _randomService = randomService;
        }

        public async Task<String> SendEmail(String post)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Slush", _configuration["EmailSettings:FromEmail"]));
            message.To.Add(new MailboxAddress("", post));
            message.Subject = "Slush";

            String code = _randomService.RandomString(6);

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<h1>Slush verification email</h1><p>Greetings, {post}, you need verificate your account by this code:<b>{code}</b></p>"
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    return code;
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
        }
    }
}
