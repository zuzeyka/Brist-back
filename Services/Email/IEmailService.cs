namespace Slush.Services.Email
{
    public interface IEmailService
    {
        Task<String> SendEmail(String post);
    }
}
