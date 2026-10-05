using Slush.Entity.Store.Product;

namespace Slush.Repositories.IRepository
{
    public interface IGameEventForGameRepository
    {
        Task<List<GameEventForGame>> GetAll();
        Task Add(GameEventForGame link);
        Task DeleteGameEventForGame(Guid id);
        Task<GameEventForGame?> GetById(Guid id);
        Task<List<GameEventForGame>> GetByGameId(Guid gameId);
    }
}
