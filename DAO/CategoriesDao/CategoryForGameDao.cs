using Slush.Data.Entity;
using Slush.Data;
using Slush.Entity.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CategoriesDao
{
    public class CategoryForGameDao
    {
        private readonly DataContext _context;

        public CategoryForGameDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryForGame>> GetAll()
        {
            return await _context.dbCategoriesForGame.Select(c => new CategoryForGame{      id = c.id,
                                                                                            gameId = c.gameId,
                                                                                            categoryId = c.categoryId,
                                                                                            createdAt = c.createdAt}).ToListAsync();
        }
        public void Add(CategoryForGame category)
        {
            _context.dbCategoriesForGame.Add(category);
            _context.SaveChanges();
        }
    }
}
