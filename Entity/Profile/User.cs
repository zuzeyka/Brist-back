using Slush.Data.Entity.Community;

using Slush.Entity.Profile;
using Slush.Entity.Store.Product;

namespace Slush.Data.Entity.Profile
{
    public class User 
    {
        public User()
        {
        }

        public User(Guid id, String? name, String? passwordSalt, String? email, String? phone, String? descripton, String? image, bool verified, float amountOfMoney, DateTime? createdAt)
        {
            this.id = id;
            this.name = name;
            this.passwordSalt = passwordSalt;
            this.email = email;
            this.phone = phone;
            this.description = descripton;
            this.image = image;
            this.verified = verified;
            this.amountOfMoney = amountOfMoney;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public String? name { get;set; }
        public String? passwordSalt { get;set; }
        public String? email { get;set; }
        public String? phone { get;set; }
        public String? description { get; set; }
        public String? image { get; set; }
        public bool verified { get; set; }
        public float amountOfMoney { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
