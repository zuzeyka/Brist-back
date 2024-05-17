<<<<<<< HEAD
﻿using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class CategoryForGame : DBRecord
    {
        public String gameId { get; set; }
        public String categoryId { get; set; }


        public CategoryForGame(String id,
                               String gameId,
                               String categoryId,
                               DateTime createdAt)
=======
﻿

namespace Slush.Data.Entity
{
    public class CategoryForGame
    {
        public CategoryForGame()
        {
        }

        public CategoryForGame(Guid id, Guid gameId, Guid categoryId, DateTime? createdAt)
>>>>>>> development_branch
        {
            this.id = id;
            this.gameId = gameId;
            this.categoryId = categoryId;
            this.createdAt = createdAt;
        }
<<<<<<< HEAD
=======



        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public Guid categoryId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }

>>>>>>> development_branch
    }
}
