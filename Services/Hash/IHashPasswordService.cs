namespace Slush.Services.Hash
{
    public interface IHashPasswordService
    {
        String Generate(String password);
        bool Verify(String password, String hashed);
    }
}
