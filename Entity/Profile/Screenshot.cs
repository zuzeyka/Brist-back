using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Profile
{
    public class Screenshot
    {
        public Screenshot(string id, string title, string? description, int likesCount, int dislikesCount, string discussionId, string gameId, string authorId, string screenshotUrl)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.dislikesCount = dislikesCount;
            this.discussionId = discussionId;
            this.gameId = gameId;
            this.authorId = authorId;
            this.screenshotUrl = screenshotUrl;
        }

        public String id { get; set; }
        public String title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public int dislikesCount { get; set; }
        public String discussionId { get; set; }
        public String gameId { get; set; }
        public String authorId { get; set; }
        public String screenshotUrl { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
