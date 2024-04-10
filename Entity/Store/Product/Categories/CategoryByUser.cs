namespace Slush.Data.Entity
{
    public class CategoryByUser
    {

        public CategoryByUser(Guid id, String name, String description, DateTime? createdAt)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
