using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Store.Product;

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
            var _gameinShopEntities = await _context.dbGamesInShops.AsNoTracking().ToListAsync();

            var _games = _gameinShopEntities.Select(g => new GameInShop(g.id,
                                                                        g.name,
                                                                        g.price,
                                                                        g.discount,
                                                                        g.previeImage,
                                                                        g.dateOfRelease,
                                                                        g.developerId,
                                                                        g.publisherId,
                                                                        g.urlForContent)).ToList();

            return _games;
        }
        public void Add(GameInShop game)
        {
            _context.dbGamesInShops.Add(game);
            _context.SaveChanges();
        }
    }
}
