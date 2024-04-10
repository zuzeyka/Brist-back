namespace FullStackBrist.Server.Models.ShopContent
{
    public class GameInShopModel
    {
        public Guid id { get; set; }
        public String name { get; set; }
        public float price { get; set; }
        public int discount { get; set; }
        public String previeImage { get; set; }
        public DateTime dateOfRelease { get; set; }
        public String developerId { get; set; }
        public String publisherId { get; set; }
        public String urlForContent { get; set; }
    }
}
