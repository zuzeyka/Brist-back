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

        public async Task UpdateDeveloper(Developer developer)
        {
            var existing = await _context.dbDevelopers.FindAsync(developer.id);
            if (existing != null)
            {
                existing.subscribersCount = developer.subscribersCount;
                existing.name = developer.name;
                existing.description = developer.description;
                existing.avatar = developer.avatar;
                existing.backgroundImage = developer.backgroundImage;
                existing.urlForNewsPage = developer.urlForNewsPage;

                await _context.SaveChangesAsync();
            }
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
            var response = await _context.dbDevelopers
                .Where(x => x.id == id)
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
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
