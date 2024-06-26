using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IAchievementByUserRepository
    {
        Task<List<AchievementByUser>> GetAllAchievements();
        Task<AchievementByUser> UpdateAchievementByUser(AchievementByUser achievementByUser);
        Task Add(AchievementByUser achievementByUser);
        Task Delete(Guid id);
        Task<AchievementByUser?> GetById(Guid id);
        Task<List<AchievementByUser?>> GetByUserId(Guid id);
        Task<List<AchievementByUser?>> GetByIds(List<Guid> guidList);
    }
}
