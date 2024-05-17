namespace Slush.Entity.Profile
{
    public class WishedGame
    {
        public WishedGame()
        {
        }

        public WishedGame(Guid id, Guid ownedGameId, Guid userId, DateTime? createdAt)
        {
            this.id = id;
            this.ownedGameId = ownedGameId;
            this.userId = userId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid ownedGameId { get; set; }
        public Guid userId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
