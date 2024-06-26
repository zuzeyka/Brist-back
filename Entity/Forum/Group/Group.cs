namespace Slush.Data.Entity.Community
{
    public class Group
    {
        public Group()
        {
        }

        public Group(Guid id, Guid attachedId, String? name, String? description, String? imageUrl, DateTime? createdAt)
        {
            this.id = id;
            this.attachedId = attachedId;
            this.name = name;
            this.description = description;
            this.imageUrl = imageUrl;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid attachedId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public String? imageUrl { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
