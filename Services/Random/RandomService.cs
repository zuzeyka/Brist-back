using System.Text;

namespace FullStackBrist.Server.Services.Random
{
    public class RandomService : IRandomService
    {
        private readonly String usableChars = new String("1234567890!@#$%^&*()-_".ToArray());

        private readonly System.Random random = new();

        public String RandomString(int length)
        {
            StringBuilder _sb = new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                int randIndex = random.Next(usableChars.Length);
                _sb.Append(usableChars[randIndex]);
            }
            return _sb.ToString();
        }

        public String ConfirmCode(int length)
        {
            StringBuilder _sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int randIndex = random.Next(usableChars.Length);
                _sb.Append(usableChars[randIndex]);
            }
            return _sb.ToString();
        }
    }
}
