namespace Slush.Entity.Profile
{
    public class AchievementByUser
    {
        public AchievementByUser() { }

        public AchievementByUser(Guid id, Guid userId, Guid achievementId, DateTime? awardTime, DateTime? createdAt)
        {
            this.id = id;
            this.userId = userId;
            this.achievementId = achievementId;
            this.awardTime = awardTime;
            this.createdAt = createdAt;
        }

        public Guid id { get;set; }
        public Guid userId { get;set; }
        public Guid achievementId { get;set; }
        public DateTime? awardTime { get; set; }

        public DateTime? createdAt { get;set; }
        public DateTime? deletedAt { get;set;}
    }
}
