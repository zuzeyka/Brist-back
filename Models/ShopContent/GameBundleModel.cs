namespace Slush.Models.ShopContent
{
    public class GameBundleModel
    {
        public Guid id { get; set; }
        public String name { get; set; } = null!;
        public String description { get; set; } = null!;
        public float price { get; set; }
        public float discount { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
