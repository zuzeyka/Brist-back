using Slush.Entity.Store.Product;

namespace Slush.Repositories.IRepository
{
    public interface IGameEventRepository
    {
        Task<List<GameEvent>> GetAll();
        Task<GameEvent> UpdateGameEvent(GameEvent gameEvent);
        Task Add(GameEvent gameEvent);
        Task DeleteGameEvent(Guid id);
        Task<GameEvent?> GetById(Guid id);
    }
}
