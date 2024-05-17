using Azure;
using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
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

        public async Task UpdateGameInShop(GameInShop shop)
        {
            var existing = await _context.dbGamesInShops.FindAsync(shop.id);
            if (existing != null)
            {
                existing.name = shop.name;
                existing.price = shop.price;
                existing.discount = shop.discount;
                existing.previeImage = shop.previeImage;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(GameInShop game)
        {
            await _context.dbGamesInShops.AddAsync(game);
            _context.SaveChanges();
        }

        public async Task DeleteGameInShop(Guid id)
        {
            var requirement = await _context.dbGamesInShops
                        .Where(x => x.id == id)
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
                        .FirstOrDefaultAsync();
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.MinValue;

                _context.Update<GameInShop>(requirement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameInShop?> GetById(Guid id)
        {
            var response = await _context.dbGamesInShops
                   .Where(x => x.id == id)
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
                   .FirstOrDefaultAsync();
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
