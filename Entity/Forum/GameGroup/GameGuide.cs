namespace Slush.Data.Entity.Community.GameGroup
{
<<<<<<< HEAD
    public class GameGuide : Slush.Entity.Abstract.Post
    {
        public String gameGroupId { get; set; }
        public String content {  get; set; }

        public GameGuide(String id,
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
            this.gameGroupId = gameGroupId;
            this.title = title;
            this.content = content;
            this.createdAt = createdAt;
        }
=======
    public class GameGuide
    {
        public GameGuide()
        {
        }

        public GameGuide(Guid id, String? title, String? description, int likesCount, Guid discussionId, Guid gameId, Guid authorId, Guid gameGroupId, String? content, DateTime? createdAt)
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
        public String? title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public int dislikesCount { get; set; }
        public Guid discussionId { get; set; }
        public Guid gameId { get; set; }
        public Guid authorId { get; set; }
        public Guid gameGroupId { get; set; }
        public String? content {  get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
