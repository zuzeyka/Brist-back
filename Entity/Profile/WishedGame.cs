using Slush.Entity.Abstract;

namespace Slush.Entity.Profile
{
    public class WishedGame :ProfileGame
    {
        public WishedGame()
        {
        }

        public WishedGame(Guid id, String ownedGameId, String userId, DateTime? createdAt)
        {
            this.id = id;
            this.ownedGameId = ownedGameId;
            this.userId = userId;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public String ownedGameId { get; set; }
        public String userId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
