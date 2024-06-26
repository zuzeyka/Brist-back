using Slush.Data.Entity.Community.GameGroup;

namespace Slush.Repositories.IRepository
{
    public interface IGameGroupRepository
    {
        Task<List<GameGroup>> GetAllGameGroups();
        Task<GameGroup> UpdateGameGroup(GameGroup group);
        Task Add(GameGroup group);
        Task DeleteGameGroup(Guid id);
        Task<GameGroup?> GetById(Guid id);
        Task<List<GameGroup?>> GetByGameId(Guid id);
        Task<List<GameGroup?>> GetByIds(List<Guid> id);
    }
}
