namespace FullStackBrist.Server.Models.Language
{
    public class LanguageInGameModel
    {
        public Guid gameId { get; set; }
        public Guid languageId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
