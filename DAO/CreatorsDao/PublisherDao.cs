using Slush.Data.Entity;
using Slush.Data;
using Slush.Entity.Store.Product.Creators;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.CreatorsDao
{
    public class PublisherDao
    {
        private readonly DataContext _context;

        public PublisherDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAllPublishers()
        {
            return await _context.dbPublishers.Select(p => new Publisher{  
                id = p.id,
                subscribersCount = p.subscribersCount,
                name = p.name,
                description = p.description,
                avatar = p.avatar,
                backgroundImage = p.backgroundImage,
                urlForNewsPage = p.urlForNewsPage, 
                createdAt = p.createdAt }).ToListAsync();
        }

        public void Add(Publisher publisher)
        {
            _context.dbPublishers.Add(publisher);
            _context.SaveChanges();
        }
    }
}
