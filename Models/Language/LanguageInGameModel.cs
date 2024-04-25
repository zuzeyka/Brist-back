namespace FullStackBrist.Server.Models.Language
{
    public class LanguageInGameModel
    {
        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public Guid languageId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
