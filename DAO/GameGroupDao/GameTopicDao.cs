using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

namespace Slush.DAO.GameGroupDao
{
    public class GameTopicDao
    {
        private readonly DataContext _context;

        public GameTopicDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameTopic>> GetAllGameTopics()
        {
            return await _context.dbGameTopics
                .Where(g => g.deleteAt == null)
                .Select(g => new GameTopic { 
                id = g.id,
                attachedId = g.attachedId,
                name = g.name,
                description = g.description,
                createdAt = g.createdAt}).ToListAsync();
        }

        public async Task UpdateGameTopic(GameTopic topic)
        {
            var existing = await _context.dbGameTopics.FindAsync(topic.id);
            if (existing != null)
            {
                existing.name = topic.name;
                existing.description = topic.description;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(GameTopic topics)
        {
            _context.dbGameTopics.Add(topics);
            _context.SaveChanges();
        }

        public async Task DeleteGameTopic(Guid id)
        {
            var requirement = await _context.dbGameTopics.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameTopic> GetById(Guid id)
        {
            var response = await _context.dbGameTopics
                .Where(x => x.id == id)
                .Select(g => new GameTopic
                {
                    id = g.id,
                    attachedId = g.attachedId,
                    name = g.name,
                    description = g.description,
                    createdAt = g.createdAt
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
