namespace FullStackBrist.Server.Models.Profile
{
    public class UserCommentModel
    {
        public Guid userId { get; set; }
        public Guid authorId { get; set; }
        public String? content { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
