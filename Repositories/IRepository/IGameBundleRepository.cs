using Slush.Entity.Store.Product;

namespace Slush.Repositories.IRepository
{
    public interface IGameBundleRepository
    {
        Task<List<GameBundle>> GetAll();
        Task<GameBundle> UpdateGameBundle(GameBundle gameBundle);
        Task DeleteGameBundle(Guid id);
        Task Add(GameBundle gameBundle);
        Task<GameBundle?> GetById(Guid id);
        Task<List<GameBundle?>> GetByBundleId(Guid id);
        Task<List<GameBundle?>> GetByBundleIds(List<Guid> id);
    }
}
