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
            var _publisherEntities = await _context.dbPublishers.AsNoTracking().ToListAsync();

            var _publishers = _publisherEntities.Select(p => new Publisher(p.id,
                                                                           p.subscribersCount,
                                                                           p.name,
                                                                           p.description,
                                                                           p.avatar,
                                                                           p.backgroundImage,
                                                                           p.urlForNewsPage, p.createdAt)).ToList();
            return _publishers;
        }

        public void Add(Publisher publisher)
        {
            _context.dbPublishers.Add(publisher);
            _context.SaveChanges();
        }
    }
}
