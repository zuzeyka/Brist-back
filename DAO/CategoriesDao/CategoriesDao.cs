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
            var _categoriesEntities = await _context.dbCategories.AsNoTracking().ToListAsync();

            var _categories = _categoriesEntities.Select(c => new Categories(c.id,
                                                                             c.name,
                                                                             c.description
                                                                             )).ToList();
            return _categories;
        }

        public void Add(Categories category)
        {
            _context.dbCategories.Add(category);
            _context.SaveChanges();
        }
    }
}
