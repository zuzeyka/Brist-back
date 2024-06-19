using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileDao
{
    public class CategoryByUserForGameDao
    {
        private readonly DataContext _context;

        public CategoryByUserForGameDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryByUserForGame>> GetAllCategoryByUserForGames()
        {
            return await _context.dbCategoryByUserForGames
                .Where(c => c.deletedAt == null)
                .Select(c => new CategoryByUserForGame
                {
                    id = c.id,
                    name = c.name,
                    image = c.image,
                    createdAt = c.createdAt
                }).ToListAsync();
        }

        public async Task<CategoryByUserForGame> UpdateCategoryByUserForGame(CategoryByUserForGame categoryByUserForGame)
        {
            var existing = await _context.dbCategoryByUserForGames.FindAsync(categoryByUserForGame.id);

            if (existing != null)
            {
                existing.name = categoryByUserForGame.name;
                existing.image = categoryByUserForGame.image;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(CategoryByUserForGame newCategoryByUserForGame)
        {
            await _context.dbCategoryByUserForGames.AddAsync(newCategoryByUserForGame);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var existing = await _context.dbCategoryByUserForGames.FindAsync(id);

            if (existing != null)
            {
                existing.deletedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<CategoryByUserForGame?> GetById(Guid id)
        {
            var response = await _context.dbCategoryByUserForGames
                .Where(x => x.id == id)
                .Select(x => new CategoryByUserForGame
                {
                    id = x.id,
                    name = x.name,
                    image = x.image,
                    createdAt = x.createdAt,
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

        public async Task<List<CategoryByUserForGame?>> GetByIds(List<Guid> ids)
        {
            List<CategoryByUserForGame> response = new List<CategoryByUserForGame> ();

            foreach(var id in ids) 
            {
                var result = await _context.dbCategoryByUserForGames
                    .Where(x => x.id == id)
                    .Select(x => new CategoryByUserForGame
                    {
                        id = x.id,
                        name = x.name,
                        image = x.image,
                        createdAt = x.createdAt,
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
