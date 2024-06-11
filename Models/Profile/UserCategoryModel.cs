namespace Slush.Models.Profile
{
    public class UserCategoryModel
    {
        public Guid userId { get; set; }
        public Guid ownedGameId { get; set; }
        public Guid categoryId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
