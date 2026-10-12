namespace Slush.Entity.Profile
{
    public enum NotificationType
    {
        FriendRequestReceived = 0,
        FriendRequestAccepted = 1,
        FriendRequestDeclined = 2,
    }

    public class Notification
    {
        public Notification() { }

        public Notification(Guid id, Guid userId, NotificationType type, Guid actorId, DateTime? createdAt)
        {
            this.id = id;
            this.userId = userId;
            this.type = type;
            this.actorId = actorId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        // The recipient — whose notification feed this shows up in.
        public Guid userId { get; set; }
        public NotificationType type { get; set; }
        // Who caused it (the other side of the friend request, etc.) — the
        // frontend resolves this to a name/avatar itself, same as everywhere else.
        public Guid actorId { get; set; }
        public bool isRead { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
