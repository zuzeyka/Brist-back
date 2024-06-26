using Slush.Data.Entity.Community.GameGroup;

namespace Slush.Repositories.IRepository
{
    public interface IGameNewsRepository
    {
        Task<List<GameNews>> GetAllGameNews();
        Task<GameNews> UpdateGameNews(GameNews news);
        Task Add(GameNews news);
        Task DeleteGameNews(Guid id);
        Task<GameNews?> GetById(Guid id);
        Task<List<GameNews?>> GetByGameId(Guid id);
        Task<List<GameNews?>> GetByIds(List<Guid> id);
    }
}
