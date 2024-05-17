namespace FullStackBrist.Server.Services.Email
{
    public interface IEmailService
    {
        bool SendMessage(String message, object model);
    }
}
