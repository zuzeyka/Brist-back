using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
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
        public async Task UpdateTopic(Topic topic)
        {
            var existing = await _context.dbTopics.FindAsync(topic.id);
            if (existing != null)
            {
                existing.name = topic.name;
                existing.description = topic.description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(Topic topic)
        {
            await _context.dbTopics.AddAsync(topic);
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

        public async Task<Topic?> GetById(Guid id)
        {
            var response = await _context.dbTopics
                .Where(x => x.id == id)
                .Select(t => new Topic
                {
                    id = t.id,
                    attachedId = t.attachedId,
                    name = t.name,
                    description = t.description,
                    authorId = t.authorId,
                    createdAt = t.createdAt
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
