using Slush.Data.Entity;

namespace Slush.Entity.Store.Product
{
    public class DLCInShop
    {

        public DLCInShop(Guid id, String gameId, String name, float price, int discount, String previeImage, List<String> gameImages, DateTime dateOfRelease, String developerId, String publisherId, DateTime? createdAt)
        {
            this.id = id;
            this.gameId = gameId;
            this.name = name;
            this.price = price;
            this.discount = discount;
            this.previeImage = previeImage;
            this.gameImages = gameImages;
            this.dateOfRelease = dateOfRelease;
            this.developerId = developerId;
            this.publisherId = publisherId;
            this.createdAt = createdAt;
        }



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

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
        public virtual List<String> categories { get; set; } = null!;
        public virtual List<LanguageInGame> languages { get; set; } = null!;
    }
}
