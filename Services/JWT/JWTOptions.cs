namespace Slush.Services.JWT
{
    public class JWTOptions
    {
        public String? SecretKey { get; set; }
        public int ExpiredHours { get; set; }
    }
}
