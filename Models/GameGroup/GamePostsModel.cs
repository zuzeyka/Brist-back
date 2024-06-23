namespace FullStackBrist.Server.Models.GameGroup
{
    public class GamePostsModel
    {
        public String? title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public Guid gameId { get; set; }
        public Guid gameTopicId { get; set; }
        public Guid authorId { get; set; }
        public String? content { get; set; }
        public String? contentUrl { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
