using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

namespace Slush.DAO.GameGroupRepository
{
    public class GameTopicRepository
    {
        private readonly DataContext _context;

        public GameTopicRepository(DataContext context)
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

        public async Task<GameTopic> UpdateGameTopic(GameTopic topic)
        {
            var existing = await _context.dbGameTopics.FindAsync(topic.id);
            if (existing != null)
            {
                existing.name = topic.name;
                existing.description = topic.description;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(GameTopic topics)
        {
            await _context.dbGameTopics.AddAsync(topics);
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

        public async Task<GameTopic?> GetById(Guid id)
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

        public async Task<List<GameTopic?>> GetByGameId(Guid id)
        {
            var response = await _context.dbGameTopics
                .Where(x => x.attachedId == id)
                .Select(g => new GameTopic
                {
                    id = g.id,
                    attachedId = g.attachedId,
                    name = g.name,
                    description = g.description,
                    createdAt = g.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GameTopic?>> GetByIds(List<Guid> id)
        {
            List<GameTopic> response = new List<GameTopic> ();

            foreach(var item in id)
            {
                var result = await _context.dbGameTopics
                .Where(x => x.id == item)
                .Select(g => new GameTopic
                {
                    id = g.id,
                    attachedId = g.attachedId,
                    name = g.name,
                    description = g.description,
                    createdAt = g.createdAt
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
