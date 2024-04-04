
namespace Slush.Data.Entity
{
    public class CategoryByAuthor
    {
        public CategoryByAuthor(string id, string name, string description, string image)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.image = image;
        }

        public String id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String image { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
