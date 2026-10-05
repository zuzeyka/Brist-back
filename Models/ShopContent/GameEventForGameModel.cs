namespace FullStackBrist.Server.Models.ShopContent
{
    public class GameEventForGameModel
    {
        public Guid gameId { get; set; }
        public Guid eventId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
