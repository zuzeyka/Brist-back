namespace FullStackBrist.Server.Models.Profile
{
    public class UserModel
    {
        public String? name { get; set; }
        public String? passwordSalt { get; set; }
        public String? email { get; set; }
        public String? phone { get; set; }
        public String? description { get; set; }
        public bool verified { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
