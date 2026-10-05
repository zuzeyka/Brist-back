using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Slush.Services.Email;

namespace FullStackBrist.Server.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendVerificationCode(String toEmail, String code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Slush", _configuration["EmailSettings:FromEmail"]));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Slush";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<h1>Slush verification email</h1><p>Greetings, {toEmail}, you need to verify your account with this code: <b>{code}</b></p>"
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

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send verification email to {Email}", toEmail);
                    return false;
                }
            }
        }
    }
}
