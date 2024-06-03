namespace Slush.Models.Profile
{
    public class SettingsModel
    {
        public Guid id {  get; set; }
        public Guid attachedUserId { get; set; }
        public bool bigSaleNotification { get; set; }
        public bool saleFromWishlistNotification { get; set; }
        public bool newCommentNotification { get; set; }
        public bool friendRequestNotification { get; set; }
        public bool approvedFriendRequestNotification { get; set; }
        public bool declinedFriendRequestNotification { get; set; }

        public DateTime? createdAt { get; set; }
    }
}
