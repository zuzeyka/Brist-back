using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Data.Entity.Community.GameGroup;

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
            return await _context.dbTopics
                .Where(t => t.deleteAt == null)
                .Select(t => new Topic {
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

        public async Task DeleteTopic(Guid id)
        {
            var requirement = await _context.dbTopics.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Topic> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbTopics.FirstOrDefault(t => t.id == id));
        }
    }
}
