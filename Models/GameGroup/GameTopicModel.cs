namespace FullStackBrist.Server.Models.GameGroup
{
    public class GameTopicModel
    {
        public Guid id { get; set; }
        public Guid attachedId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public DateTime createdAt { get; set; }
    }
}
