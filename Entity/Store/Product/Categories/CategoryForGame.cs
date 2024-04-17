using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class CategoryForGame
    {
        public CategoryForGame()
        {
        }

        public CategoryForGame(Guid id, String gameId, String categoryId, DateTime? createdAt)
        {
            this.id = id;
            this.gameId = gameId;
            this.categoryId = categoryId;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public String gameId { get; set; }
        public String categoryId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }

    }
}
