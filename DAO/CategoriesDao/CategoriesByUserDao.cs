using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CategoriesDao
{
    public class CategoriesByUserDao
    {
        private readonly DataContext _context;

        public CategoriesByUserDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryByUser>> GetAllCategoriesByUser()
        {
            return await _context.dbCategoriesByUsers
                .Where(c => c.deleteAt == null)
                .Select(c => new CategoryByUser
            {
                id = c.id,
                name = c.name,
                description = c.description,
                createdAt = c.createdAt
            }).ToListAsync();

        }

        public async Task UpdateCategoriesByUser(CategoryByUser category)
        {
            var existing = await _context.dbCategoriesByUsers.FindAsync(category.id);
            if (existing != null)
            {
                existing.name = category.name;
                existing.description = category.description;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(CategoryByUser category)
        {
            _context.dbCategoriesByUsers.Add(category);
            _context.SaveChanges();
        }

        public async Task DeleteCategoryByUser(Guid id)
        {
            var requirement = await _context.dbCategoriesByUsers.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CategoryByUser> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbCategoriesByUsers.FirstOrDefault(c => c.id == id));
        }
    }
}
