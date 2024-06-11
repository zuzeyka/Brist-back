namespace FullStackBrist.Server.Models.Profile
{
    public class FriendsModel
    {
        public Guid userId { get; set; }
        public Guid friendId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
