namespace FullStackBrist.Server.Models.Profile
{
    public class FriendsModel
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public Guid friendId { get; set; }
    }
}
