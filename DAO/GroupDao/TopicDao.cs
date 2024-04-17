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
            return await _context.dbTopics.Select(t => new Topic {
                id = t.id,
                attachedId = t.attachedId,
                name = t.name,
                description = t.description,
                authorId = t.authorId,
                createdAt = t.createdAt}).ToListAsync();
        }
        public void Add(Topic topic)
        {
            _context.dbTopics.Add(topic);
            _context.SaveChanges();
        }
    }
}
