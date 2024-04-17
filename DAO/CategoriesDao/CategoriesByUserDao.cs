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
            return await _context.dbCategoriesByUsers.Select(c => new CategoryByUser
            {
                id = c.id,
                name = c.name,
                description = c.description,
                createdAt = c.createdAt
            }).ToListAsync();

        }
        public void Add(CategoryByUser category)
        {
            _context.dbCategoriesByUsers.Add(category);
            _context.SaveChanges();
        }
    }
}
