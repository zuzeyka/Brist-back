namespace FullStackBrist.Server.Models.Profile
{
    public class UserCommentModel
    {
        public Guid id { get; set; }
        public String userId { get; set; }
        public Guid authorId { get; set; }
        public String content { get; set; }
    }
}
