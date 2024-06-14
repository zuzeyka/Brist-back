namespace Slush.Models
{
    public class DiscussionModel
    {
        public Guid authordId { get; set; }
        public Guid attachedId { get; set; }
        public String? content { get; set; }
        public int likesCount { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
