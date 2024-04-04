using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class LanguageInGame
    {
        public LanguageInGame(string id, string gameId, string languageId)
        {
            this.id = id;
            this.gameId = gameId;
            this.languageId = languageId;
        }

        public String id { get; set; }
        public String gameId { get; set; }
        public String languageId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
