using System.Runtime.InteropServices;

namespace Slush.Services.Hash
{
    public class HashPasswordService
    {
        public String Generate(String password)
        {
            String hashed = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            
            return hashed;
        }

        public bool Verify(String password, String hashed)
        {
            bool result = BCrypt.Net.BCrypt.EnhancedVerify(password, hashed);

            return result;
        }
    }
}
