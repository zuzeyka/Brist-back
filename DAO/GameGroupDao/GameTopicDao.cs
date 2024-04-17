using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.dbGameTopics.Select(g => new GameTopic { 
                id = g.id,
                attachedId = g.attachedId,
                name = g.name,
                description = g.description,
                createdAt = g.createdAt}).ToListAsync();
        }
        public void Add(GameTopic topics)
        {
            _context.dbGameTopics.Add(topics);
            _context.SaveChanges();
        }
    }
}
