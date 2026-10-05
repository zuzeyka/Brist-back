namespace Slush.Services.Email
{
    public interface IEmailService
    {
        Task<bool> SendVerificationCode(String toEmail, String code);
    }
}
