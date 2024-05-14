namespace FullStackBrist.Server.Models.ShopContent
{
    public class DLCInShopModel
    {
        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public String name { get; set; }
        public float price { get; set; }
        public int discount { get; set; }
        public String previeImage { get; set; }
        public DateTime dateOfRelease { get; set; }
        public Guid developerId { get; set; }
        public Guid publisherId { get; set; }
        public String urlForContent { get; set; }
        public DateTime createdAt { get; set; }
    }
}
