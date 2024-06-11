namespace FullStackBrist.Server.Models.Group
{
    public class GroupModel
    {
        public Guid attachedId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
