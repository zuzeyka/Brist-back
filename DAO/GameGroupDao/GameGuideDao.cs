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
            return await _context.dbGameGuides.Select(g => new GameGuide{ id = g.id,
                title = g.title,
                description = g.description,
                likesCount = g.likesCount,
                discussionId = g.discussionId,
                gameId = g.gameId,
                authorId = g.authorId,
                gameGroupId = g.gameGroupId,
                content = g.content, 
                createdAt = g.createdAt }).ToListAsync();

        }
        public void Add(GameGuide guide)
        {
            _context.dbGameGuides.Add(guide);
            _context.SaveChanges();
        }
    }
}
