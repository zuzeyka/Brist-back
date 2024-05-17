namespace FullStackBrist.Server.Services.Random
{
    public interface IRandomService
    {
        String RandomString(int lengthOfSymbols);
        String ConfirmCode(int lengthOfSymbols);
    }
}
