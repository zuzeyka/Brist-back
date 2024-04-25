using Slush.Data.Entity;
using Slush.Data;
using Slush.Entity.Store.Product;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math;
using Microsoft.AspNetCore.Http.HttpResults;
using Slush.Data.Entity.Community.GameGroup;

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
            return await _context.dbDLCsInShop
                .Where(d => d.deleteAt == null)
                .Select(d => new DLCInShop{
                id = d.id,
                gameId = d.gameId,
                name = d.name,
                price = d.price,
                discount = d.discount,
                previeImage = d.previeImage,
                gameImages = d.gameImages,
                dateOfRelease = d.dateOfRelease,
                developerId = d.developerId,
                publisherId = d.publisherId,
                createdAt = d.createdAt}).ToListAsync();
        }
        public void Add(DLCInShop dlc)
        {
            _context.dbDLCsInShop.Add(dlc);
            _context.SaveChanges();
        }

        public async Task DeleteDLCInShop(Guid id)
        {
            var requirement = await _context.dbDLCsInShop.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DLCInShop> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbDLCsInShop.FirstOrDefault(d => d.id == id));
        }
    }
}
