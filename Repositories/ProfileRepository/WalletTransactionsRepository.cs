using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.ProfileRepository
{
    public class WalletTransactionsRepository : IWalletTransactions
    {
        private DataContext _context;

        public WalletTransactionsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<WalletTransactions>> GetAll()
        {
            return await _context.dbWalletTransactions
                .Where(w => w.deletedAt == null)
                .Select(w => new WalletTransactions() { 
                    id = w.id,
                    userId = w.userId,
                    transactionObj = w.transactionObj,
                    currency = w.currency,
                    createdAt = w.createdAt
                }).ToListAsync();
        }

        public async Task<WalletTransactions> UpdateWalletTransactions(WalletTransactions transaction)
        {
            var existing = await _context.dbWalletTransactions.FindAsync(transaction.id);
            if(existing != null)
            {
                existing.userId = transaction.userId;
                existing.transactionObj = transaction.transactionObj;
                existing.currency = transaction.currency;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task DeleteWalletTransaction(Guid id)
        {
            var existing = await _context.dbWalletTransactions.FindAsync(id);
            if(existing != null)
            {
                existing.deletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(WalletTransactions transaction)
        {
            await _context.dbWalletTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<WalletTransactions?> GetById(Guid id)
        {
            var response = await _context.dbWalletTransactions
                .Where(x => x.id == id)
                .Select(w => new WalletTransactions
                {
                    id = w.id,
                    userId = w.userId,
                    transactionObj = w.transactionObj,
                    currency = w.currency
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<WalletTransactions?> GetByUserId(Guid id)
        {
            var response = await _context.dbWalletTransactions
                .Where(x => x.id == id)
                .Select(w => new WalletTransactions
                {
                    id = w.id,
                    userId = w.userId,
                    transactionObj = w.transactionObj,
                    currency = w.currency
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<WalletTransactions?>> GetByIds(List<Guid> ids)
        {
            List<WalletTransactions> response = new List<WalletTransactions> ();

            foreach (var id in ids)
            {
                var result = await _context.dbWalletTransactions
                    .Where(x => x.id == id)
                    .Where(c => c.deletedAt == null)
                    .Select(w => new WalletTransactions
                    {
                        id = w.id,
                        userId = w.userId,
                        transactionObj = w.transactionObj,
                        currency = w.currency
                    }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
