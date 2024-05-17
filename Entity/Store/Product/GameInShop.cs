using Slush.Data.Entity;


namespace Slush.Entity.Store.Product
{
    public class GameInShop
    {

        public GameInShop(Guid id, String? name, float price, int discount, String? previeImage, DateTime dateOfRelease, Guid developerId, Guid publisherId, String? urlForContent, DateTime? createdAt)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.discount = discount;
            this.previeImage = previeImage;
            this.dateOfRelease = dateOfRelease;
            this.developerId = developerId;
            this.publisherId = publisherId;
            this.urlForContent = urlForContent;
            this.createdAt = createdAt;
        }

        public GameInShop() { }

        public Guid id { get; set; }
        public String? name { get; set; }
        public float price { get; set; }
        public int discount { get; set; }
        public String? previeImage { get; set; }
        public DateTime dateOfRelease { get; set; }
        public Guid developerId { get; set; }
        public Guid publisherId { get; set; }
        public String? urlForContent { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
