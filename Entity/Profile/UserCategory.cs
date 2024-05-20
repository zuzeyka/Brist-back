namespace Slush.Entity.Profile
{
    public class UserCategory
    {
        public UserCategory() { }

        public UserCategory(Guid id, Guid userId, Guid ownedGameId, Guid categoryId, DateTime? createdAt)
        {
            this.id = id;
            this.userId = userId;
            this.ownedGameId = ownedGameId;
            this.categoryId = categoryId;
            this.createdAt = createdAt;
        }

        public Guid id {  get; set; }
        public Guid userId { get; set; }
        public Guid ownedGameId { get; set; }
        public Guid categoryId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
