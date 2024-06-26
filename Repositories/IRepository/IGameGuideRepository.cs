using Slush.Data.Entity.Community.GameGroup;

namespace Slush.Repositories.IRepository
{
    public interface IGameGuideRepository
    {
        Task<List<GameGuide>> GetAllGameGuides();
        Task<GameGuide> UpdateGameGuide(GameGuide guide);
        Task Add(GameGuide guide);
        Task DeleteGameGuide(Guid id);
        Task<GameGuide?> GetById(Guid id);
        Task<List<GameGuide?>> GetByGameId(Guid id);
        Task<List<GameGuide?>> GetByIds(List<Guid> id);
    }
}
