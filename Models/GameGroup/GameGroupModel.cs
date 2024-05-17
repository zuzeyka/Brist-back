namespace FullStackBrist.Server.Models.GameGroup
{
    public class GameGroupModel
    {
        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
