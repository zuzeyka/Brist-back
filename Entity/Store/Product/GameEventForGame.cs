namespace Slush.Entity.Store.Product
{
    public class GameEventForGame
    {
        public GameEventForGame()
        {
        }

        public GameEventForGame(Guid id, Guid gameId, Guid eventId, DateTime? createdAt)
        {
            this.id = id;
            this.gameId = gameId;
            this.eventId = eventId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public Guid eventId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
