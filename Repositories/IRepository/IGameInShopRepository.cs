using Slush.Entity.Store.Product;

namespace Slush.Repositories.IRepository
{
    public interface IGameInShopRepository
    {
        Task<List<GameInShop>> GetAll();
        Task<GameInShop> UpdateGameInShop(GameInShop shop);
        Task<String> Add(GameInShop game);
        Task<GameInShop> GetGameInShopByName(String name);
        Task DeleteGameInShop(Guid id);
        Task<GameInShop?> GetById(Guid id);
        Task<List<GameInShop?>> GetByIds(List<Guid> id);
    }
}
