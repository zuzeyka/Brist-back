namespace FullStackBrist.Server.Models.Group
{
    public class GroupModel
    {
        public Guid id { get; set; }
        public String attachedId { get; set; }
        public String name { get; set; }
        public String description { get; set; }
    }
}
