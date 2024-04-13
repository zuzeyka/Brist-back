using Slush.Data.Entity;
using Slush.Data;
using Slush.Entity.Store.Product.Creators;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CreatorsDao
{
    public class DeveloperDao
    {
        private readonly DataContext _context;

        public DeveloperDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Developer>> GetAllDevelopersDao()
        {
            var _developersEntities = await _context.dbDevelopers.AsNoTracking().ToListAsync();

            var _developers = _developersEntities.Select(d => new Developer(d.id,
                                                                            d.subscribersCount,
                                                                            d.name,
                                                                            d.description,
                                                                            d.avatar,
                                                                            d.backgroundImage,
                                                                            d.urlForNewsPage, d.createdAt)).ToList();

            return _developers;
        }
        public void Add(Developer developer)
        {
            _context.dbDevelopers.Add(developer);
            _context.SaveChanges();
        }
    }
}
