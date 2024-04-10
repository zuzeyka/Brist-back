namespace FullStackBrist.Server.Models.Profile
{
    public class UserModel
    {
        public Guid id { get; set; }
        public String name { get; set; }
        public String passwordSalt { get; set; }
        public String salt { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
    }
}
