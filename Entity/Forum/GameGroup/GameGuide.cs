namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameGuide
    {
        public GameGuide(Guid id, String title, String? description, int likesCount, String discussionId, String gameId, String authorId, String gameGroupId, String content, DateTime? createdAt)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.discussionId = discussionId;
            this.gameId = gameId;
            this.authorId = authorId;
            this.gameGroupId = gameGroupId;
            this.content = content;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public String title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public int dislikesCount { get; set; }
        public String discussionId { get; set; }
        public String gameId { get; set; }
        public String authorId { get; set; }
        public String gameGroupId { get; set; }
        public String content {  get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
        public virtual GameGroup gameGroup { get; set; } = null!;
    }
}
