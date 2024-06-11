namespace FullStackBrist.Server.Models.Profile
{
    public class WishedGameModel
    {
        public Guid ownedGameId { get; set; }
        public Guid userId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
