using Slush.Entity.Store.Product;

namespace Slush.Repositories.IRepository
{
    public interface IGameBundleCollectionRepository
    {
        Task<List<GameBundleCollection>> GetAll();
        Task<GameBundleCollection> UpdateGameBundleCollection(GameBundleCollection gameBundleCollection);
        Task DeleteGameBundleCollection(Guid id);
        Task Add(GameBundleCollection gameBundleCollection);
        Task<GameBundleCollection?> GetById(Guid id);
        Task<List<GameBundleCollection?>> GetByGameId(Guid id);
        Task<List<GameBundleCollection?>> GetByGameIds(List<Guid> id);
    }
}
