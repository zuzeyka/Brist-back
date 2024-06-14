using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Store.Product;

namespace Slush.DAO.GameInShopDao
{
    public class GameBundleDao
    {
        private readonly DataContext _context;

        public GameBundleDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameBundle>> GetAll()
        {
            return await _context.dbGameBundles
                .Where(g => g.deletedAt == null)
                .Select(g => new GameBundle
                {
                    id = g.id,
                    name = g.name,
                    description = g.description,
                    price = g.price,
                    discount = g.discount,
                    createdAt = g.createdAt
                }).ToListAsync();
        }

        public async Task UpdateGameBundle(GameBundle gameBundle)
        {
            var existing = await _context.dbGameBundles.FindAsync(gameBundle.id);
            if (existing != null)
            {
                existing.name = gameBundle.name;
                existing.description = gameBundle.description;
                existing.price = gameBundle.price;
                existing.discount = gameBundle.discount;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGameBundle(Guid id)
        {
            var existing = await _context.dbGameBundles.FindAsync(id);
            if(existing != null)
            {
                existing.deletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(GameBundle gameBundle)
        {
            await _context.dbGameBundles.AddAsync(gameBundle);
            await _context.SaveChangesAsync();
        }

        public async Task<GameBundle?> GetById(Guid id)
        {
            var response = await _context.dbGameBundles
                .Where(x => x.id == id)
                .Select(g => new GameBundle
                {
                    id = g.id,
                    name = g.name,
                    description = g.description,
                    price = g.price,
                    discount = g.discount,
                    createdAt = g.createdAt
                }).FirstOrDefaultAsync();
            if(response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GameBundle?>> GetByBundleId(Guid id)
        {
            var response = await _context.dbGameBundles
                .Where(x => x.id == id)
                .Select(g => new GameBundle
                {
                    id = g.id,
                    name = g.name,
                    description = g.description,
                    price = g.price,
                    discount = g.discount,
                    createdAt = g.createdAt
                }).ToListAsync();
            if(response != null)
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
