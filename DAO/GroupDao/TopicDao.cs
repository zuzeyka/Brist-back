using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;

namespace Slush.DAO.GroupDao
{
    public class TopicDao
    {
        private readonly DataContext _context;

        public TopicDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            var _topicEntities = await _context.dbTopics.AsNoTracking().ToListAsync();

            var _topics = _topicEntities.Select(t => new Topic(t.id,
                                                                t.attachedId,
                                                                t.name,
                                                                t.description,
                                                                t.authorId, t.createdAt)).ToList();

            return _topics;
        }
        public void Add(Topic topic)
        {
            _context.dbTopics.Add(topic);
            _context.SaveChanges();
        }
    }
}
