using Slush.Data.Entity.Profile;


namespace Slush.Data.Entity.Community
{
    public class Topic
    {
        public Topic()
        {
        }

        public Topic(Guid id, Guid attachedId, String name, String description, Guid authorId, Guid postId, DateTime? createdAt)
        {
            this.id = id;
            this.attachedId = attachedId;
            this.name = name;
            this.description = description;
            this.authorId = authorId;
            this.postId = postId;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public Guid attachedId { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public Guid authorId { get; set; }
        public Guid postId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }

    }
}
