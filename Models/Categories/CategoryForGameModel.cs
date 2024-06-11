namespace FullStackBrist.Server.Models.Categories
{
    public class CategoryForGameModel
    {
        public Guid gameId { get; set; }
        public Guid categoryId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
