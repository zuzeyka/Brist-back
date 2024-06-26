using Slush.Entity.Store.Product;

namespace Slush.Repositories.IRepository
{
    public interface IDLCInShopRepository
    {
        Task<List<DLCInShop>> GetAll();
        Task<DLCInShop> UpdateDLCInShop(DLCInShop dlc);
        Task Add(DLCInShop dlc);
        Task DeleteDLCInShop(Guid id);
        Task<DLCInShop?> GetById(Guid id);
        Task<List<DLCInShop?>> GetByGameId(Guid id);
        Task<List<DLCInShop>> GetDlcsByIds(List<Guid> id);
    }
}
