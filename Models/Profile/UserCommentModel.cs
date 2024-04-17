namespace FullStackBrist.Server.Models.Profile
{
    public class UserCommentModel
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public Guid authorId { get; set; }
        public String content { get; set; }
    }
}
