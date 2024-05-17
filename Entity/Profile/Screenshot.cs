<<<<<<< HEAD
﻿using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Profile
{
    public class Screenshot : Post
    {
        public String screenshotUrl { get; set; }

        public Screenshot(String id,
                         String authorId,
                         String gameId,
                         String discussionId,
                         int dislikesCount,
                         int likesCount,
                         String? description,
                         String screenshotUrl,
                         String title,
                         DateTime createdAt)
        {
            this.id = id;
            this.likesCount = likesCount;
            this.gameId = gameId;
            this.authorId = authorId;
            this.screenshotUrl = screenshotUrl;
            this.discussionId = discussionId;
            this.dislikesCount = dislikesCount;
            this.description = description;
            this.title = title;
            this.createdAt = createdAt;
        }
=======
﻿

namespace Slush.Data.Entity.Profile
{
    public class Screenshot
    {
        public Screenshot()
        {
        }

        public Screenshot(Guid id, String? title, String? description, int likesCount, int dislikesCount, Guid discussionId, Guid gameId, Guid authorId, String? screenshotUrl, DateTime? createdAt)
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
        public String? screenshotUrl { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
