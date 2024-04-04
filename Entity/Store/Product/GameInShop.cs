using Slush.Data.Entity;
using Slush.Entity.Abstract;

namespace Slush.Entity.Store.Product
{
    public class GameInShop
    {
        public GameInShop(string id, string name, float price, int discount, string previeImage, DateTime dateOfRelease, string developerId, string publisherId, string urlForContent)
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
        }

        public String id { get; set; }
        public String name { get; set; }
        public float price { get; set; }
        public int discount { get; set; }
        public String previeImage { get; set; }
        public DateTime dateOfRelease { get; set; }
        public String developerId { get; set; }
        public String publisherId { get; set; }
        public String urlForContent { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }

        public virtual List<String> categoriesId { get; set; }           = null!;
        public virtual List<String> gameImages { get; set; }           = null!;
        public virtual List<LanguageInGame> languages { get; set; }    = null!;
        public virtual List<String> languagesId { get; set; }          = null!;
        public virtual List<String> systemRequirementsId { get; set; } = null!;
    }
}
