
namespace Slush.Data.Entity
{
    public class CategoryByAuthor
    {
        public CategoryByAuthor()
        {
        }

        public CategoryByAuthor(Guid id, Guid authorId, String? name, String? description, String? image, DateTime? createdAt)
        {
            this.id = id;
            this.authorId = authorId;
            this.name = name;
            this.description = description;
            this.image = image;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public Guid authorId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public String? image { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
