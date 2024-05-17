using Slush.Data.Entity;
<<<<<<< HEAD
using Slush.Entity.Abstract;

namespace Slush.Entity.Store.Product
{
    public class DLCInShop : Game
    {
        public String gameId;
        public DLCInShop(String id,
                         String name,
                         float price,
                         String image,
                         DateTime dateOfRelease,
                         String author,
                         String publisher,
                         List<String> categories,
                         List<LanguageInGame> languages,
                         String urlForContent,
                         DateTime createdAt,
                         String gameId)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.previeImage = image;
            this.dateOfRelease = dateOfRelease;
            this.developerId = author;
            this.publisherId = publisher;
            this.categoriesId = categories;
            this.urlForContent = urlForContent;
            this.createdAt = createdAt;
            this.gameId = gameId;
        }
=======

namespace Slush.Entity.Store.Product
{
    public class DLCInShop
    {
        public DLCInShop()
        {
        }

        public DLCInShop(Guid id, Guid gameId, String? name, float price, int discount, String? previeImage, DateTime dateOfRelease, Guid developerId, Guid publisherId, DateTime? createdAt)
        {
            this.id = id;
            this.gameId = gameId;
            this.name = name;
            this.price = price;
            this.discount = discount;
            this.previeImage = previeImage;
            this.dateOfRelease = dateOfRelease;
            this.developerId = developerId;
            this.publisherId = publisherId;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public Guid gameId { get; set; } 
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
>>>>>>> development_branch
    }
}
