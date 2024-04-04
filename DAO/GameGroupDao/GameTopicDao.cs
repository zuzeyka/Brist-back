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
            var _gameTopicEntities = await _context.dbGameTopics.AsNoTracking().ToListAsync();

            var _gameTopics = _gameTopicEntities.Select(g => new GameTopic(g.id,
                                                                            g.attachedId,
                                                                            g.name,
                                                                            g.description)).ToList();
            return _gameTopics;
        }

        public void Add(GameTopic topics)
        {
            _context.dbGameTopics.Add(topics);
            _context.SaveChanges();
        }
    }
}
