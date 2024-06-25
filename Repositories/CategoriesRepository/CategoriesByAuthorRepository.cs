using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CategoriesRepository
{
    public class CategoriesByAuthorRepository
    {
        private readonly DataContext _context;

        public CategoriesByAuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryByAuthor>> GetAllCategoriesByAuthor()
        {
            return await _context.dbCategoriesByAuthors
                .Where(c => c.deleteAt == null)
                .Select(c => new CategoryByAuthor
            {
                id = c.id,
                name = c.name,
                description = c.description,
                image = c.image,
                createdAt = c.createdAt
            }).ToListAsync();
        }

        public async Task<CategoryByAuthor> UpdateCategoriesByAuthor(CategoryByAuthor category)
        {
            var existing = await _context.dbCategoriesByAuthors.FindAsync(category.id);
            if(existing != null)
            {
                existing.name = category.name; 
                existing.description = category.description; 
                existing.image = category.image;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(CategoryByAuthor category)
        {
            await _context.dbCategoriesByAuthors.AddAsync(category);
            _context.SaveChanges();
        }

        public async Task DeleteCategoryByAuthor(Guid id)
        {
            var requirement = await _context.dbCategoriesByAuthors.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<CategoryByAuthor?> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbCategoriesByAuthors.FirstOrDefault(c => c.id == id));
        }
        public async Task<List<CategoryByAuthor>> GetAllCategoriesByIds(List<Guid> guidList)
        {
            List<CategoryByAuthor> response = new List<CategoryByAuthor>();
            foreach (var category in guidList)
            {
                var result = await _context.dbCategoriesByAuthors
                .Where(c => c.deleteAt == null)
                .Where(c => c.id == category)
                .Select(c => new CategoryByAuthor
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                    image = c.image,
                    createdAt = c.createdAt
                }).FirstOrDefaultAsync();

                if (result != null)
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
