namespace Slush.Entity.Profile
{
    public enum FriendRequestStatus
    {
        Pending = 0,
        Accepted = 1,
    }

    public class Friends
    {
        public Friends()
        {
        }

        public Friends(Guid id, Guid userId, Guid friendId, DateTime? createdAt, FriendRequestStatus status = FriendRequestStatus.Pending)
        {
            this.id = id;
            this.userId = userId;
            this.friendId = friendId;
            this.createdAt = createdAt;
            this.status = status;
        }

        public Guid id { get; set; }
        // The sender of the request — the recipient is friendId. Once status is
        // Accepted, the two are symmetric and this distinction no longer matters.
        public Guid userId { get; set; }
        public Guid friendId { get; set; }
        public FriendRequestStatus status { get; set; } = FriendRequestStatus.Pending;

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
