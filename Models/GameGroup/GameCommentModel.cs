namespace FullStackBrist.Server.Models.GameGroup
{
    public class GameCommentModel
    {
        public Guid gamePostId { get; set; }
        public String? content { get; set; }
        public Guid authorId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
