namespace Slush.Data.Entity
{
    public class CategoryByUser
    {
        public CategoryByUser()
        {
        }

        public CategoryByUser(Guid id, Guid authorId, String? name, String? description, DateTime? createdAt)
        {
            this.id = id;
            this.authorId = authorId;
            this.name = name;
            this.description = description;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public Guid authorId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
