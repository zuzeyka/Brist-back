using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Entity.Store.Product;
using System.Text.Json;

namespace Slush.DAO.GameInShopDao
{
    public class GameInShopDao
    {
        private readonly DataContext _context;

        public GameInShopDao(DataContext context)
        {
            _context = context;
        }
        public async Task<List<GameInShop>> GetAll()
        {
            return await _context.dbGamesInShops
                .Where(g => g.deleteAt == null)
                                        .Select(g => new GameInShop
                                        {
                                            id = Guid.Parse(g.id.ToString()),
                                            name = g.name,
                                            price = g.price,
                                            discount = g.discount,
                                            previeImage = g.previeImage,
                                            dateOfRelease = g.dateOfRelease,
                                            developerId = g.developerId,
                                            publisherId = g.publisherId,
                                            urlForContent = g.urlForContent,
                                            createdAt = g.createdAt
                                        })
                                        .ToListAsync();
        }
        public void Add(GameInShop game)
        {
            _context.dbGamesInShops.Add(game);
            _context.SaveChanges();
        }

        public async Task DeleteGameInShop(Guid id)
        {
            var requirement = await _context.dbGamesInShops.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameInShop> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbGamesInShops.FirstOrDefault(g => g.id == id));
        }
    }
}
