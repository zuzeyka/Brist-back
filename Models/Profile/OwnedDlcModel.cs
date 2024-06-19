namespace Slush.Models.Profile
{
    public class OwnedDlcModel
    {
        public Guid ownedDlcId { get; set; }
        public Guid userId { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
