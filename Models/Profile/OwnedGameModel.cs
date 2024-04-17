namespace FullStackBrist.Server.Models.Profile
{
    public class OwnedGameModel
    {
        public Guid id { get; set; }
        public Guid ownedGameId { get; set; }
        public Guid userId { get; set; }
    }
}
