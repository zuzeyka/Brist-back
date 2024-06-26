using Slush.Data;
using Slush.Entity.Store.Product;
using Microsoft.EntityFrameworkCore;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.GameInShopRepository
{
    public class DLCInShopRepository : IDLCInShopRepository
    {
        private readonly DataContext _context;

        public DLCInShopRepository(DataContext context)
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
                discountFinish = d.discountFinish,
                previeImage = d.previeImage,
                description = d.description,
                dateOfRelease = d.dateOfRelease,
                developerId = d.developerId,
                publisherId = d.publisherId,
                createdAt = d.createdAt}).ToListAsync();
        }
        public async Task<DLCInShop> UpdateDLCInShop(DLCInShop dlc)
        {
            var existing = await _context.dbDLCsInShop.FindAsync(dlc.id);
            if (existing != null)
            {
                existing.name = dlc.name;
                existing.price = dlc.price;
                existing.discount = dlc.discount;
                existing.discountFinish = dlc.discountFinish;
                existing.previeImage = dlc.previeImage;
                existing.description = dlc.description;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(DLCInShop dlc)
        {
            await _context.dbDLCsInShop.AddAsync(dlc);
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

        public async Task<DLCInShop?> GetById(Guid id)
        {
            var response =  await _context.dbDLCsInShop
                .Where(x => x.id == id)
                .Select(d => new DLCInShop
                {
                    id = d.id,
                    gameId = d.gameId,
                    name = d.name,
                    price = d.price,
                    discount = d.discount,
                    discountFinish = d.discountFinish,
                    previeImage = d.previeImage,
                    description = d.description,
                    dateOfRelease = d.dateOfRelease,
                    developerId = d.developerId,
                    publisherId = d.publisherId,
                    createdAt = d.createdAt
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

        public async Task<List<DLCInShop?>> GetByGameId(Guid id)
        {
            var response =  await _context.dbDLCsInShop
                .Where(x => x.gameId == id)
                .Where(c => c.deleteAt == null)
                .Select(d => new DLCInShop
                {
                    id = d.id,
                    gameId = d.gameId,
                    name = d.name,
                    price = d.price,
                    discount = d.discount,
                    discountFinish = d.discountFinish,
                    previeImage = d.previeImage,
                    description = d.description,
                    dateOfRelease = d.dateOfRelease,
                    developerId = d.developerId,
                    publisherId = d.publisherId,
                    createdAt = d.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<DLCInShop>> GetDlcsByIds(List<Guid> id)
        {
            List<DLCInShop> response = new List<DLCInShop> ();

            foreach (var dlc in id)
            {
                var result = await _context.dbDLCsInShop
                .Where(x => x.id == dlc)
                .Where(c => c.deleteAt == null)
                .Select(d => new DLCInShop
                {
                    id = d.id,
                    gameId = d.gameId,
                    name = d.name,
                    price = d.price,
                    discount = d.discount,
                    discountFinish = d.discountFinish,
                    previeImage = d.previeImage,
                    description = d.description,
                    dateOfRelease = d.dateOfRelease,
                    developerId = d.developerId,
                    publisherId = d.publisherId,
                    createdAt = d.createdAt
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
