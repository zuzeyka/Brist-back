namespace Slush.Data.Entity
{
<<<<<<< HEAD
    public class CategoryByUser : Categories
    {
        public CategoryByUser(String id,
                              String name,
                              String description,
                              DateTime createdAt)
=======
    public class CategoryByUser
    {
        public CategoryByUser()
        {
        }

        public CategoryByUser(Guid id, String? name, String? description, DateTime? createdAt)
>>>>>>> development_branch
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.createdAt = createdAt;
        }
<<<<<<< HEAD
=======



        public Guid id { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
