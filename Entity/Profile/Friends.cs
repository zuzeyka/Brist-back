namespace Slush.Entity.Profile
{
    public class Friends
    {
        public Friends(string id, string userId, string friendId)
        {
            this.id = id;
            this.userId = userId;
            this.friendId = friendId;
        }

        public String id { get; set; }
        public String userId { get; set; }
        public String friendId { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
