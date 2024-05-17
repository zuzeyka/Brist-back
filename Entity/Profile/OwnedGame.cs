<<<<<<< HEAD
﻿using Slush.Entity.Abstract;

namespace Slush.Entity.Profile
{
    public class OwnedGame : ProfileGame
    {
        public OwnedGame(string id,
                         string ownedGameId,
                         string userId,
                         DateTime createdAt)
=======
﻿

namespace Slush.Entity.Profile
{
    public class OwnedGame
    {
        public OwnedGame()
        {
        }

        public OwnedGame(Guid id, Guid ownedGameId, Guid userId, DateTime? createdAt)
>>>>>>> development_branch
        {
            this.id = id;
            this.ownedGameId = ownedGameId;
            this.userId = userId;
            this.createdAt = createdAt;
        }
<<<<<<< HEAD
=======
        public Guid id { get; set; }
        public Guid ownedGameId { get; set; }
        public Guid userId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
