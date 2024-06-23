using Slush.Data.Entity.Profile;

namespace Slush.Data.Entity.Community
{
    public class Post
    {
        public Post()
        {
        }

        public Post(Guid id, String? title, String? description, int likesCount, Guid discussionId, Guid authorId, String? content, String? contentUrl, DateTime? createdAt)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.discussionId = discussionId;
            this.authorId = authorId;
            this.content = content;
            this.contentUrl = contentUrl;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String? title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public Guid discussionId { get; set; }
        public Guid authorId { get; set; }
        public String? content { get; set; }
        public String? contentUrl { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
