using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CategoriesDao
{
    public class CategoriesByAuthorDao
    {
        private readonly DataContext _context;

        public CategoriesByAuthorDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryByAuthor>> GetAllCategoriesByAuthor()
        {
            return await _context.dbCategoriesByAuthors.Select(c => new CategoryByAuthor
            {
                id = c.id,
                name = c.name,
                description = c.description,
                image = c.image,
                createdAt = c.createdAt
            }).ToListAsync();

        }
        public void Add(CategoryByAuthor category)
        {
            _context.dbCategoriesByAuthors.Add(category);
            _context.SaveChanges();
        }
    }
}
