using Slush.Data.Entity.Profile;


namespace Slush.Data.Entity.Community
{
    public class Topic
    {
        public Topic()
        {
        }

        public Topic(Guid id, Guid attachedId, String? name, String? description, Guid authorId, DateTime? createdAt)
        {
            this.id = id;
            this.attachedId = attachedId;
            this.name = name;
            this.description = description;
            this.authorId = authorId;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public Guid attachedId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public Guid authorId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }

    }
}
