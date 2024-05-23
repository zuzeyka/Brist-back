namespace FullStackBrist.Server.Models.Group
{
    public class TopicModel
    {
        public Guid id { get; set; }
        public Guid attachedId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public Guid authorId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
