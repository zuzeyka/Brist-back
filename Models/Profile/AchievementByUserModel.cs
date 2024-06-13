namespace Slush.Models.Profile
{
    public class AchievementByUserModel
    {
        public Guid userId { get; set; }
        public Guid achievementId { get; set; }

        public DateTime? createdAt { get; set; }
    }
}
