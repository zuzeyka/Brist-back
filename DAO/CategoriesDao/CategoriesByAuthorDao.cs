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
            var _categoriesByAuthorEntities = await _context.dbCategoriesByAuthors.AsNoTracking().ToListAsync();
            
            var _categoriesByAuthor = _categoriesByAuthorEntities.Select(c => new CategoryByAuthor(c.id,
                                                                                                    c.name,
                                                                                                    c.description,
                                                                                                    c.image)).ToList();
            return _categoriesByAuthor;
}

        public void Add(CategoryByAuthor category)
        {
            _context.dbCategoriesByAuthors.Add(category);
            _context.SaveChanges();
        }
    }
}
