using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface ISettingsRepository
    {
        Task<List<Settings>> GetAll();
        Task<Settings> UpdateSettings(Settings newSettings);
        Task DeleteSettings(Guid id);
        Task Add(Settings settings);
        Task<Settings?> GetById(Guid id);
        Task<Settings?> GetByUserId(Guid id);
        Task<List<Settings?>> GetByIds(List<Guid> ids);
    }
}
