
namespace Slush.Data.Entity
{
<<<<<<< HEAD
    public class CategoryByAuthor : Categories
    {
        public String image { get; set; }
        public CategoryByAuthor(String id,
                                String name,
                                String description,
                                String image,
                                DateTime createdAt)
=======
    public class CategoryByAuthor
    {
        public CategoryByAuthor()
        {
        }

        public CategoryByAuthor(Guid id, String? name, String? description, String? image, DateTime? createdAt)
>>>>>>> development_branch
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.image = image;
            this.createdAt = createdAt;
        }
<<<<<<< HEAD
=======



        public Guid id { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public String? image { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
