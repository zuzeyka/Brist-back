using Slush.Data.Entity;
using Slush.Data;

using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CategoriesDao
{
    public class CategoryForGameDao
    {
        private readonly DataContext _context;

        public CategoryForGameDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryForGame>> GetAll()
        {
            return await _context.dbCategoriesForGame
                .Where(c => c.deleteAt == null)
                .Select(c => new CategoryForGame{      id = c.id,
                                                                                            gameId = c.gameId,
                                                                                            categoryId = c.categoryId,
                                                                                            createdAt = c.createdAt}).ToListAsync();
        }

        public async Task UpdateCategoryForGame(CategoryForGame category)
        {
            var existing = await _context.dbCategoriesForGame.FindAsync(category.id);
            if (existing != null)
            {
                existing.gameId = category.gameId;
                existing.categoryId = category.categoryId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(CategoryForGame category)
        {
            await _context.dbCategoriesForGame.AddAsync(category);
            _context.SaveChanges();
        }

        public async Task DeleteCategoryForGame(Guid id)
        {
            var requirement = await _context.dbCategoriesForGame.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CategoryForGame?> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbCategoriesForGame.FirstOrDefault(c => c.id == id));
        }
    }
}
