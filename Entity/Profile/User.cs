namespace Slush.Data.Entity.Profile
{
    public class User 
    {
        public User()
        {
        }

        public User(Guid id, String? name, String? passwordSalt, String? email, String? descripton, String? image, bool verified, float amountOfMoney, float amountOfXp, DateTime? createdAt)
        {
            this.id = id;
            this.name = name;
            this.passwordSalt = passwordSalt;
            this.email = email;
            this.description = descripton;
            this.image = image;
            this.verified = verified;
            this.amountOfMoney = amountOfMoney;
            this.amountOfXp = amountOfXp;
            this.createdAt = createdAt;
        }


        public Guid id { get; set; }
        public String? name { get;set; }
        public String? passwordSalt { get;set; }
        public String? email { get;set; }
        public String? description { get; set; }
        public String? image { get; set; }
        public bool verified { get; set; }
        public float amountOfMoney { get; set; }
        public float amountOfXp { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
