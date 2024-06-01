namespace Slush.Models.ShopContent
{
    public class GameBundleCollectionModel
    {
        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public Guid bundleId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
