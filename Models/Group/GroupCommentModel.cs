namespace FullStackBrist.Server.Models.Group
{
    public class GroupCommentModel
    {
        public Guid id { get; set; }
        public String groupId { get; set; }
        public String content { get; set; }
        public Guid userId { get; set; }
    }
}
