<<<<<<< HEAD
﻿using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Profile
{
    public class Video : Post
    {
        public String videoUrl { get; set; }

        public Video(String id,
                         String authorId,
                         String gameId,
                         String discussionId,
                         int dislikesCount,
                         int likesCount,
                         String? description,
                         String videoUrl,
                         String title,
                         DateTime createdAt)
        {
            this.id = id;
            this.likesCount = likesCount;
            this.gameId = gameId;
            this.authorId = authorId;
            this.videoUrl = videoUrl;
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
    public class Video
    {
        public Video()
        {
        }

        public Video(Guid id, String? title, String? description, int likesCount, int dislikesCount, Guid gameId, Guid authorId, String? videoUrl, DateTime? createdAt)
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
        public String? title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public int dislikesCount { get; set; }
        public Guid discussionId { get; set; }
        public Guid gameId { get; set; }
        public Guid authorId { get; set; }
        public String? videoUrl { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
