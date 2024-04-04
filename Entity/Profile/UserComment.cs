using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Profile
{
    public class UserComment
    {
        public UserComment(string id, string userId, Guid authorId, string content)
        {
            this.id = id;
            this.userId = userId;
            this.authorId = authorId;
            this.content = content;
        }

        public String id { get; set; }
        public String userId { get; set; }
        public Guid authorId { get; set; }
        public String content { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
