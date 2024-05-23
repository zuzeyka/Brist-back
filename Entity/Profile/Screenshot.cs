

namespace Slush.Data.Entity.Profile
{
    public class Screenshot
    {
        public Screenshot()
        {
        }

        public Screenshot(Guid id, String? title, String? description, int likesCount, Guid gameId, Guid authorId, String? screenshotUrl, DateTime? createdAt)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.likesCount = likesCount;
            this.gameId = gameId;
            this.authorId = authorId;
            this.screenshotUrl = screenshotUrl;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String? title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public Guid gameId { get; set; }
        public Guid authorId { get; set; }
        public String? screenshotUrl { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
