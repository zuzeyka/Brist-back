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
            return await _context.dbDevelopers
                .Where(d => d.deleteAt == null)
                .Select(d => new Developer
            {
                id = d.id,
                subscribersCount = d.subscribersCount,
                name = d.name,
                description = d.description,
                avatar = d.avatar,
                backgroundImage = d.backgroundImage,
                urlForNewsPage = d.urlForNewsPage,
                createdAt = d.createdAt
            }).ToListAsync();

        }
        public void Add(Developer developer)
        {
            _context.dbDevelopers.Add(developer);
            _context.SaveChanges();
        }

        public async Task DeleteDeveloper(Guid id)
        {
            var requirement = await _context.dbDevelopers.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Developer> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbDevelopers.FirstOrDefault(d => d.id == id));
        }
    }
}
