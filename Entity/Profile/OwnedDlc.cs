namespace Slush.Entity.Profile
{
    public class OwnedDlc
    {
        public OwnedDlc()
        {
        }

        public OwnedDlc(Guid id, Guid ownedDlcId, Guid userId, DateTime? createdAt)
        {
            this.id = id;
            this.ownedDlcId = ownedDlcId;
            this.userId = userId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid ownedDlcId { get; set; }
        public Guid userId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
