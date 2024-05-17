using Slush.Data.Entity.Profile;

using Slush.Entity.Store.Product;

namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameGroup
    {
        public GameGroup()
        {
        }

        public GameGroup(Guid id, Guid gameId, DateTime? createdAt)
        {
            this.id = id;
            this.gameId = gameId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
