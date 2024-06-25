using Slush.Data.Entity;
using Slush.Data;
using Slush.Entity.Store.Product.Creators;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CreatorsRepository
{
    public class PublisherRepository
    {
        private readonly DataContext _context;

        public PublisherRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAllPublishers()
        {
            return await _context.dbPublishers
                .Where(p => p.deleteAt == null)
                .Select(p => new Publisher{  
                id = p.id,
                subscribersCount = p.subscribersCount,
                name = p.name,
                description = p.description,
                avatar = p.avatar,
                backgroundImage = p.backgroundImage,
                urlForNewsPage = p.urlForNewsPage, 
                createdAt = p.createdAt }).ToListAsync();
        }

        public async Task<Publisher> UpdatePublisher(Publisher publisher)
        {
            var existing = await _context.dbPublishers.FindAsync(publisher.id);
            if (existing != null)
            {
                existing.subscribersCount = publisher.subscribersCount;
                existing.name = publisher.name;
                existing.description = publisher.description;
                existing.avatar = publisher.avatar;
                existing.backgroundImage = publisher.backgroundImage;
                existing.urlForNewsPage = publisher.urlForNewsPage;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(Publisher publisher)
        {
            await _context.dbPublishers.AddAsync(publisher);
            _context.SaveChanges();
        }

        public async Task DeletePublisher(Guid id)
        {
            var requirement = await _context.dbPublishers.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Publisher?> GetById(Guid id)
        {
            var response = await _context.dbPublishers
                .Where(x => x.id == id)
                .Select(p => new Publisher
                {
                    id = p.id,
                    subscribersCount = p.subscribersCount,
                    name = p.name,
                    description = p.description,
                    avatar = p.avatar,
                    backgroundImage = p.backgroundImage,
                    urlForNewsPage = p.urlForNewsPage,
                    createdAt = p.createdAt
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


        public async Task<List<Publisher?>> GetByIds(List<Guid> ids)
        {
            List<Publisher> response = new List<Publisher> ();

            foreach(var id in ids) 
            {
                var result = await _context.dbPublishers
                .Where(x => x.id == id)
                .Select(p => new Publisher
                {
                    id = p.id,
                    subscribersCount = p.subscribersCount,
                    name = p.name,
                    description = p.description,
                    avatar = p.avatar,
                    backgroundImage = p.backgroundImage,
                    urlForNewsPage = p.urlForNewsPage,
                    createdAt = p.createdAt
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
