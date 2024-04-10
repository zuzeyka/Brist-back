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
            var _categoryForGameEntities = await _context.dbCategoriesForGame.AsNoTracking().ToListAsync();

            var _categoryForGame = _categoryForGameEntities.Select(c => new CategoryForGame(c.id,
                                                                                            c.gameId,
                                                                                            c.categoryId)).ToList();
            return _categoryForGame;
        }
        public void Add(CategoryForGame category)
        {
            _context.dbCategoriesForGame.Add(category);
            _context.SaveChanges();
        }
    }
}
