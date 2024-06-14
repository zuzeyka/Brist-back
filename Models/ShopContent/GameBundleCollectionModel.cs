namespace Slush.Models.ShopContent
{
    public class GameBundleCollectionModel
    {
        public Guid gameId { get; set; }
        public Guid dlcId { get; set; }
        public Guid bundleId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
