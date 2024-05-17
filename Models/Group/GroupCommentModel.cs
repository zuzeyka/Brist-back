namespace FullStackBrist.Server.Models.Group
{
    public class GroupCommentModel
    {
        public Guid id { get; set; }
        public Guid groupId { get; set; }
        public String? content { get; set; }
        public Guid userId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
