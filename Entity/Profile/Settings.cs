namespace Slush.Entity.Profile
{
    public class Settings
    {
        public Settings() { }

        public Settings(Guid id, Guid attachedUserId, bool bigSaleNotification, bool saleFromWishlistNotification, bool newCommentNotification, bool friendRequestNotification, bool approvedFriendRequest, bool declinedFriendRequest, DateTime? createdAt)
        {
            this.id = id;
            this.attachedUserId = attachedUserId;
            this.bigSaleNotification = bigSaleNotification;
            this.saleFromWishlistNotification = saleFromWishlistNotification;
            this.newCommentNotification = newCommentNotification;
            this.friendRequestNotification = friendRequestNotification;
            this.approvedFriendRequest = approvedFriendRequest;
            this.declinedFriendRequest = declinedFriendRequest;
            this.createdAt = createdAt;
        }

        public Guid id {  get; set; }
        public Guid attachedUserId { get; set; }
        public bool bigSaleNotification { get; set; }
        public bool saleFromWishlistNotification { get; set; }
        public bool newCommentNotification { get; set; }
        public bool friendRequestNotification { get; set; }
        public bool approvedFriendRequest { get; set; }
        public bool declinedFriendRequest { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
