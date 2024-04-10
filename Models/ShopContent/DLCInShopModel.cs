namespace FullStackBrist.Server.Models.ShopContent
{
    public class DLCInShopModel
    {
        public Guid id { get; set; }
        public String gameId { get; set; }
        public String name { get; set; }
        public float price { get; set; }
        public int discount { get; set; }
        public String previeImage { get; set; }
        public virtual List<String> gameImages { get; set; }
        public DateTime dateOfRelease { get; set; }
        public String developerId { get; set; }
        public String publisherId { get; set; }
        public virtual List<String> categoriesId { get; set; }
        public virtual List<String> languagesId { get; set; }
        public virtual List<String> systemRequirementsId { get; set; }
        public String urlForContent { get; set; }
    }
}
