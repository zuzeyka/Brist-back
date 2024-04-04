using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class CategoryForGame
    {
        public CategoryForGame(string id, string gameId, string categoryId)
        {
            this.id = id;
            this.gameId = gameId;
            this.categoryId = categoryId;
        }

        public String id { get; set; }
        public String gameId { get; set; }
        public String categoryId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }

    }
}
