namespace Slush.Entity.Profile
{
    public class Friends
    {
        public Friends()
        {
        }

        public Friends(Guid id, Guid userId, Guid friendId, DateTime? createdAt)
        {
            this.id = id;
            this.userId = userId;
            this.friendId = friendId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid userId { get; set; }
        public Guid friendId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
