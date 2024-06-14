namespace Slush.Entity.Store.Product
{
    public class GameBundle
    {
        public GameBundle() { }

        public GameBundle(Guid id, String name, String description, float price, float discount, DateTime? discountFinish, DateTime? createdAt) 
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
            this.discount = discount;
            this.discountFinish = discountFinish;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String name { get; set; } = null!;
        public String description { get; set; } = null!;
        public float price { get; set; }
        public float discount { get; set; }
        public DateTime? discountFinish { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }

    }
}
