

namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameComment
    {
        public GameComment()
        {
        }

        public GameComment(Guid id, Guid gamePostId, String content, Guid authorId, DateTime? createdAt)
        {
            this.id = id;
            this.gamePostId = gamePostId;
            this.content = content;
            this.authorId = authorId;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public Guid gamePostId { get; set; }
        public String content { get; set; }
        public Guid authorId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
