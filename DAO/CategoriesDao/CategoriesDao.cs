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

        public async Task Add(Categories category)
        {
            await _context.dbCategories.AddAsync(category);
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

        public async Task<Categories?> GetById(Guid id)
        {
            var response = await _context.dbCategories
                .Where(x => x.id == id)
                .Where(a => a.deleteAt == null)
                .Select(c => new Categories
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                    createdAt = c.createdAt
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
        public async Task<List<Categories?>> GetByIds(List<Guid> id)
        {
            List<Categories> response = new List<Categories> ();

            foreach(var item in id)
            {
                var result = await _context.dbCategories
                .Where(x => x.id == item)
                .Where(a => a.deleteAt == null)
                .Select(c => new Categories
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                    createdAt = c.createdAt
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
