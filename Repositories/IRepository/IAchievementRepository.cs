using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IAchievementRepository
    {
        Task<List<Achievement>> GetAllAchievements();
        Task<Achievement> UpdateAchievement(Achievement achievement);
        Task Add(Achievement achievement);
        Task Delete(Guid id);
        Task<Achievement?> GetById(Guid id);
        Task<List<Achievement?>> GetByAllIds(List<Guid> id);
    }
}
