namespace FullStackBrist.Server.Services.Email
{
    public interface IEmailService
    {
        bool SendMessage(string message, object model);
    }
}
