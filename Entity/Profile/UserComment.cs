

namespace Slush.Data.Entity.Profile
{
    public class UserComment
    {
        public UserComment()
        {
        }

        public UserComment(Guid id, Guid userId, Guid authorId, String? content, DateTime? createdAt)
        {
            this.id = id;
            this.userId = userId;
            this.authorId = authorId;
            this.content = content;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid userId { get; set; }
        public Guid authorId { get; set; }
        public String? content { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
