using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CategoriesDao
{
    public class CategoriesDAO
    {
        private readonly DataContext _context;

        public CategoriesDAO(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Categories>> GetAll()
        {
            return await _context.dbCategories
                .Where(c => c.deleteAt == null)
                .Select(c => new Categories{   id = c.id,
                                                                             name = c.name,
                                                                             description = c.description,
                                                                             createdAt = c.createdAt
            }).ToListAsync();
        }

        public async Task UpdateCategories(Categories category)
        {
            var existing = await _context.dbCategories.FindAsync(category.id);
            if (existing != null)
            {
                existing.name = category.name;
                existing.description = category.description;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(Categories category)
        {
            _context.dbCategories.Add(category);
            _context.SaveChanges();
        }

        public async Task DeleteCategories(Guid id)
        {
            var requirement = await _context.dbCategories.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Categories> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbCategories.FirstOrDefault(c => c.id == id));
        }
    }
}
