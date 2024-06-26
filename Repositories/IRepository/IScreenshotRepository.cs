using Slush.Data.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IScreenshotRepository
    {
        Task<List<Screenshot>> GetAllScreenshots();
        Task<Screenshot> UpdateScreenshot(Screenshot screenshot);
        Task Add(Screenshot screenshot);
        Task DeleteScreenshot(Guid id);
        Task<Screenshot?> GetById(Guid id);
        Task<List<Screenshot?>> GetByGameId(Guid id);
        Task<List<Screenshot?>> GetByUserId(Guid id);
        Task<List<Screenshot?>> GetByIds(List<Guid> ids);
    }
}
