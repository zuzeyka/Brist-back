using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Profile
{
    public class Video
    {
        public Video()
        {
        }

        public Video(Guid id, String title, String? description, int likesCount, int dislikesCount, String gameId, String authorId, String videoUrl, DateTime? createdAt)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.dislikesCount = dislikesCount;
            this.gameId = gameId;
            this.authorId = authorId;
            this.videoUrl = videoUrl;
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
        public String videoUrl { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
