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
            var _categoriesByUserEntity = await _context.dbCategoriesByUsers.AsNoTracking().ToListAsync();

            var _categoriesByUser = _categoriesByUserEntity.Select(c => new CategoryByUser(c.id,
                                                                        c.name,
                                                                        c.description,
                                                                        c.createdAt
                                                                        )).ToList();
            return _categoriesByUser;
        }
        public void Add(CategoryByUser category)
        {
            _context.dbCategoriesByUsers.Add(category);
            _context.SaveChanges();
        }
    }
}
