using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.GameGroupDao
{
    public class GameGuideDao
    {
        private readonly DataContext _context;

        public GameGuideDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameGuide>> GetAllGameGuides()
        {
            var _gameGuidesEntity = await _context.dbGameGuides.AsNoTracking().ToListAsync();

            var _gameGuides = _gameGuidesEntity.Select(g => new GameGuide(g.id,
                                                                          g.title,
                                                                          g.description,
                                                                          g.likesCount,
                                                                          g.discussionId,
                                                                          g.gameId,
                                                                          g.authorId,
                                                                          g.gameGroupId,
                                                                          g.content, g.createdAt)).ToList();

            return _gameGuides;
        }
        public void Add(GameGuide guide)
        {
            _context.dbGameGuides.Add(guide);
            _context.SaveChanges();
        }
    }
}
