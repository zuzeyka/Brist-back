namespace Slush.Data.Entity
{
    public class CategoryByUser
    {
        public CategoryByUser(string id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public String id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
