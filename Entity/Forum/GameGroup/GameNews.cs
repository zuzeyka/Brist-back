namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameNews
    {
        public GameNews(string id, string title, string? description, int likesCount, int dislikesCount, string gameId, string authorId, string content)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.dislikesCount = dislikesCount;
            this.gameId = gameId;
            this.authorId = authorId;
            this.content = content;
        }

        public String id { get; set; }
        public String title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public int dislikesCount { get; set; }
        public String discussionId { get; set; }
        public String gameId { get; set; }
        public String authorId { get; set; }
        public String content { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
        public virtual GameGroup gameGroup { get; set; } = null!;
    }
}
