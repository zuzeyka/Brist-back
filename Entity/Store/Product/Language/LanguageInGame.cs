using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class LanguageInGame
    {
        public LanguageInGame()
        {
        }

        public LanguageInGame(Guid id, String gameId, String languageId, DateTime? createdAt)
        {
            this.id = id;
            this.gameId = gameId;
            this.languageId = languageId;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public String gameId { get; set; }
        public String languageId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
