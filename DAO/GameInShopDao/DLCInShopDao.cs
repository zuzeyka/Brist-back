using Slush.Data.Entity;
using Slush.Data;
using Slush.Entity.Store.Product;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.GameInShopDao
{
    public class DLCInShopDao
    {
        private readonly DataContext _context;

        public DLCInShopDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<DLCInShop>> GetAll()
        {
            var _dlcInShopEntities = await _context.dbDLCsInShop.AsNoTracking().ToListAsync();
            var _dlcs = _dlcInShopEntities.Select(d => new DLCInShop(d.id,
                                                                     d.gameId,
                                                                     d.name,
                                                                     d.price,
                                                                     d.discount,
                                                                     d.previeImage,
                                                                     d.gameImages,
                                                                     d.dateOfRelease,
                                                                     d.developerId,
                                                                     d.publisherId)).ToList();
            return _dlcs;
        }

        public void Add(DLCInShop dlc)
        {
            _context.dbDLCsInShop.Add(dlc);
            _context.SaveChanges();
        }
    }
}
