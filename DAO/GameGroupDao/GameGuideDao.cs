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
            return await _context.dbGameGuides
                .Where(g => g.deleteAt == null)
                .Select(g => new GameGuide{ id = g.id,
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

        public async Task DeleteGameGuide(Guid id)
        {
            var requirement = await _context.dbGameGuides.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameGuide> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbGameGuides.FirstOrDefault(g => g.id == id));
        }
    }
}
