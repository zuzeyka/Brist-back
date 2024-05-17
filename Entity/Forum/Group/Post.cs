using Slush.Data.Entity.Profile;

namespace Slush.Data.Entity.Community
{
<<<<<<< HEAD
    public class Post : Slush.Entity.Abstract.Post
    {
        public String content { get; set; }

        public Post(String id,
                    String authorId,
                    String? description,
                    String gameId,
                    String content,
                    String discussionId,
                    DateTime createdAt,
                    int likesCount,
                    int dislikesCount)
        {
            this.id = id;
            this.authorId = authorId;
            this.content = content;
            this.description = description;
            this.discussionId = discussionId;
            this.createdAt = createdAt;
            this.likesCount = likesCount;
            this.dislikesCount = dislikesCount;
            this.gameId = gameId;
        }
=======
    public class Post
    {
        public Post()
        {
        }

        public Post(Guid id, String? title, String? description, int likesCount, int dislikesCount, Guid discussionId, Guid gameId, Guid authorId, String? content, DateTime? createdAt)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.dislikesCount = dislikesCount;
            this.discussionId = discussionId;
            this.gameId = gameId;
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
        public Guid authorId { get; set; }
        public String? content { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
