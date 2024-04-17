namespace Slush.Entity.Profile
{
    public class Friends
    {
        public Friends()
        {
        }

        public Friends(Guid id, String userId, String friendId, DateTime? createdAt)
        {
            this.id = id;
            this.userId = userId;
            this.friendId = friendId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String userId { get; set; }
        public String friendId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
