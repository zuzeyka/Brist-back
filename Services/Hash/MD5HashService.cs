namespace FullStackBrist.Server.Services.Hash
{
    public class MD5HashService : IHashService
    {
        public String Hash(String text)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            return
                Convert.ToHexString
                    (md5.ComputeHash
                        (System.Text.Encoding.UTF8.GetBytes
                            (text)));
        }
    }
}
