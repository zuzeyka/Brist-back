namespace Slush.Data.Entity.Community.GameGroup
{
<<<<<<< HEAD
    public class GameNews : Slush.Entity.Abstract.Post
    {
        public String content { get; set; }

        public GameNews(String id,
                         String authorId,
                         String gameId,
                         String discussionId,
                         int dislikesCount,
                         int likesCount,
                         String? description,
                         String gameGroupId,
                         String title,
                         String content,
                         DateTime createdAt)
        {
            this.id = id;
            this.likesCount = likesCount;
            this.gameId = gameId;
            this.authorId = authorId;
            this.discussionId = discussionId;
            this.dislikesCount = dislikesCount;
            this.description = description;
            this.title = title;
            this.content = content;
            this.createdAt = createdAt;
        }
=======
    public class GameNews
    {
        public GameNews()
        {
        }

        public GameNews(Guid id, String? title, String? description, int likesCount, int dislikesCount, Guid gameId, Guid gameGroupId, Guid authorId, String? content, DateTime? createdAt)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.dislikesCount = dislikesCount;
            this.gameId = gameId;
            this.gameGroupId = gameGroupId;
            this.authorId = authorId;
            this.content = content;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String? title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public int dislikesCount { get; set; }
        public Guid discussionId { get; set; }
        public Guid gameId { get; set; }
        public Guid gameGroupId { get; set; }
        public Guid authorId { get; set; }
        public String? content { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
