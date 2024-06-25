using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Store.Product;

namespace Slush.Repositories.GameInShopRepository
{
    public class GameInShopRepository
    {
        private readonly DataContext _context;

        public GameInShopRepository(DataContext context)
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
                                            discountFinish = g.discountFinish,
                                            previeImage = g.previeImage,
                                            description = g.description,
                                            dateOfRelease = g.dateOfRelease,
                                            developerId = g.developerId,
                                            publisherId = g.publisherId,
                                            urlForContent = g.urlForContent,
                                            createdAt = g.createdAt
                                        })
                                        .ToListAsync();
        }

        public async Task<GameInShop> UpdateGameInShop(GameInShop shop)
        {
            var existing = await _context.dbGamesInShops.FindAsync(shop.id);
            if (existing != null)
            {
                existing.name = shop.name;
                existing.price = shop.price;
                existing.discount = shop.discount;
                existing.discountFinish = shop.discountFinish;
                existing.previeImage = shop.previeImage;
                existing.description = shop.description;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task<String> Add(GameInShop game)
        {
            var result = GetGameInShopByName(game.name);

            if (result != null)
            {
                return "Game is already exist";
            }

            await _context.dbGamesInShops.AddAsync(game);
            _context.SaveChanges();

            return "Game added";
        }

        public async Task<GameInShop> GetGameInShopByName(String name)
        {
            var response = await _context.dbGamesInShops
                   .Where(x => x.name == name)
                   .Select(g => new GameInShop
                   {
                       id = Guid.Parse(g.id.ToString()),
                       name = g.name,
                       price = g.price,
                       discount = g.discount,
                       discountFinish = g.discountFinish,
                       previeImage = g.previeImage,
                       description = g.description,
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
                            discountFinish = g.discountFinish,
                            previeImage = g.previeImage,
                            description = g.description,
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
                       discountFinish = g.discountFinish,
                       previeImage = g.previeImage,
                       description = g.description,
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

        public async Task<List<GameInShop?>> GetByIds(List<Guid> id)
        {
            List<GameInShop> response = new List<GameInShop> ();

            foreach(var item in id)
            {
                var result = await _context.dbGamesInShops
                   .Where(x => x.id == item)
                   .Select(g => new GameInShop
                   {
                       id = Guid.Parse(g.id.ToString()),
                       name = g.name,
                       price = g.price,
                       discount = g.discount,
                       discountFinish = g.discountFinish,
                       previeImage = g.previeImage,
                       description = g.description,
                       dateOfRelease = g.dateOfRelease,
                       developerId = g.developerId,
                       publisherId = g.publisherId,
                       urlForContent = g.urlForContent,
                       createdAt = g.createdAt
                   })
                   .FirstOrDefaultAsync();

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
