using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.CategoriesRepository
{
    public class CategoriesByUserRepository : ICategoriesByUserRepository
    {
        private readonly DataContext _context;

        public CategoriesByUserRepository(DataContext context)
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

        public async Task<CategoryByUser> UpdateCategoriesByUser(CategoryByUser category)
        {
            var existing = await _context.dbCategoriesByUsers.FindAsync(category.id);
            if (existing != null)
            {
                existing.name = category.name;
                existing.description = category.description;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(CategoryByUser category)
        {
            await _context.dbCategoriesByUsers.AddAsync(category);
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

        public async Task<CategoryByUser?> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbCategoriesByUsers.Where(c => c.deleteAt == null).FirstOrDefault(c => c.id == id));
        }

        public async Task<List<CategoryByUser?>> GetAllById(List<Guid> ids)
        {
            List<CategoryByUser> response = new List<CategoryByUser>();

            foreach(var item in ids)
            {
                var result = await _context.dbCategoriesByUsers
                .Where(c => c.deleteAt == null)
                .Select(c => new CategoryByUser
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

            if(response != null)
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
