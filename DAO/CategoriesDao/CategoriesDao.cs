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
            return await _context.dbCategories.Select(c => new Categories{   id = c.id,
                                                                             name = c.name,
                                                                             description = c.description,
                                                                             createdAt = c.createdAt
            }).ToListAsync();
        }

        public void Add(Categories category)
        {
            _context.dbCategories.Add(category);
            _context.SaveChanges();
        }
    }
}
