using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IWalletTransactions
    {
        Task<List<WalletTransactions>> GetAll();
        Task<WalletTransactions> UpdateWalletTransactions(WalletTransactions transaction);
        Task DeleteWalletTransaction(Guid id);
        Task Add(WalletTransactions transaction);
        Task<WalletTransactions?> GetById(Guid id);
        Task<WalletTransactions?> GetByUserId(Guid id);
        Task<List<WalletTransactions?>> GetByIds(List<Guid> ids);
    }
}
