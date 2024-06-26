using Slush.Data;
using Slush.Entity.Store.Product.Creators;
using Microsoft.EntityFrameworkCore;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.CreatorsRepository
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly DataContext _context;

        public DeveloperRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Developer>> GetAllDevelopersRepositories()
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

        public async Task<Developer> UpdateDeveloper(Developer developer)
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

            return existing;
        }

        public async Task Add(Developer developer)
        {
            await _context.dbDevelopers.AddAsync(developer);
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

        public async Task<Developer?> GetById(Guid id)
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

        public async Task<List<Developer>> GetByIds(List<Guid> ids)
        {
            List<Developer> response = new List<Developer>();

            foreach (var id in ids)
            {
                var result = await _context.dbDevelopers
                .Where(x => x.id == id)
                .Where(c => c.deleteAt == null)
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

                if(result != null)
                {
                    response.Add(result);
                }
            }
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
