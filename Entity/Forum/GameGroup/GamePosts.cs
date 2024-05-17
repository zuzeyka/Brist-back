using Slush.Data.Entity.Profile;

namespace Slush.Data.Entity.Community.GameGroup
{
<<<<<<< HEAD
    public class GamePosts : Slush.Entity.Abstract.Post
    {
        public String content { get; set; }
        public List<GameComment> comments { get; set; }

        public GamePosts(String id,
                         String authorId,
                         String gameId,
                         String discussionId,
                         int dislikesCount,
                         int likesCount,
                         String? description,
                         String title,
                         String content,
                         List<GameComment> comments,
                         DateTime createdAt)
        {
            this.id = id;
            this.likesCount = likesCount;
            this.comments = comments;
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
    public class GamePosts
    {
        public GamePosts()
        {
        }

        public GamePosts(Guid id, String? title, String? description, int likesCount, int dislikesCount, Guid discussionId, Guid gameId, Guid gameTopicId, Guid authorId, String? content, DateTime? createdAt)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.dislikesCount = dislikesCount;
            this.discussionId = discussionId;
            this.gameId = gameId;
            this.gameTopicId = gameTopicId;
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
        public Guid gameTopicId { get; set; }
        public Guid authorId { get; set; }
        public String? content { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
