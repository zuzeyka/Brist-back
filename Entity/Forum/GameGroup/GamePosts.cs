namespace Slush.Data.Entity.Community.GameGroup
{
    public class GamePosts
    {
        public GamePosts()
        {
        }

        public GamePosts(Guid id, String? title, String? description, int likesCount,  Guid gameId, Guid authorId, String? content, String? contentUrl, DateTime? createdAt)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.gameId = gameId;
            this.authorId = authorId;
            this.content = content;
            this.contentUrl = contentUrl;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String? title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public Guid gameId { get; set; }
        public Guid authorId { get; set; }
        public String? content { get; set; }
        public String? contentUrl { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
