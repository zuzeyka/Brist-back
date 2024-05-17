using System.Net;
using System.Net.Mail;

namespace FullStackBrist.Server.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendMessage(String message, object model)
        {
            String? template = null;
            String[] filenames = new String[]
            {
                message,
                message + ".html",
                "Services/Email/" + message,
                "Services/Email/" + message + ".html"
            };

            foreach (String filename in filenames)
            {
                if(File.Exists(filename))
                {
                    template = File.ReadAllText(filename);
                    break;
                }
            }

            if (template == null)
            {
                throw new ArgumentException($"Template '{message}' is not found");
            }

            String? host = _configuration["Smtp:Gmail:Host"];
            if(host == null)
            {
                throw new MissingFieldException($"Missing Smtp:Gmail:Host");
            }

            String? mailbox = _configuration["Smtp:Gmail:Email"];
            if(mailbox == null)
            {
                throw new MissingFieldException($"Missing Smtp:Gmail:Email");
            }

            String? password = _configuration["Smtp:Gmail:Password"];
            if(password == null)
            {
                throw new MissingFieldException($"Missing Smtp:Gmail:Password");
            }

            int port;
            try
            {
                port = Convert.ToInt32(_configuration["Smtp:Gmail:Port"]);
            }
            catch
            {
                throw new MissingFieldException("Missing Smtp:Gmail:Port");
            }

            bool ssl;
            try
            {
                ssl = Convert.ToBoolean(_configuration["Smtp:Gmail:Ssl"]);
            }
            catch 
            {
                throw new MissingFieldException("Smtp:Gmail:Ssl");
            }

            String? userMail = null;
            foreach (var prop in model.GetType().GetProperties())
            {
                if(prop.Name == "Email")
                {
                    userMail = prop.GetValue(model)?.ToString();
                }
                String placeholder = $"{{{{{prop.Name}}}}}";
                if (template.Contains(placeholder))
                {
                    template = template.Replace(placeholder, prop.GetValue(model)?.ToString() ?? "");
                }
            }

            if(userMail == null)
            {
                throw new ArgumentException("No 'Email' property in model");
            }

            using SmtpClient _smtpClient = new(host, port)

            {
                EnableSsl = ssl,
                Credentials = new NetworkCredential(userMail, password)
            };

            MailMessage mailMessage = new()
            {
                From = new MailAddress(mailbox),
                Subject = "Brist",
                Body = template,
                IsBodyHtml = true
            };

            mailMessage.To.Add(userMail);

            try
            {
                _smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception: ", ex);
            }
        }
    }
}
