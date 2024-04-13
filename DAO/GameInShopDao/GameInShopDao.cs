using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Store.Product;
using System.Text.Json;

namespace Slush.DAO.GameInShopDao
{
    public class GameInShopDao
    {
        private readonly DataContext _context;
        private readonly ILogger<GameInShopDao> _logger;

        public GameInShopDao(DataContext context, ILogger<GameInShopDao> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<DateTime>> GetAllNames()
        {
            return await _context.dbGamesInShops.Select(g => g.dateOfRelease).ToListAsync();
        }
        public async Task<List<GameInShop>> GetAll()
        {
            return await _context.dbGamesInShops
                                        .Select(g => new GameInShop
                                        {
                                            id = g.id,
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
    }
}
