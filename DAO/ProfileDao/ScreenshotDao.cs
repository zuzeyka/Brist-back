using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Profile;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileDao
{
    public class ScreenshotDao
    {
        private readonly DataContext _context;

        public ScreenshotDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Screenshot>> GetAllScreenshots()
        {
            return await _context.dbScreenshots.Select(s => new Screenshot {
                id = s.id,
                title = s.title,
                description = s.description,
                likesCount = s.likesCount,
                dislikesCount = s.dislikesCount,
                discussionId = s.discussionId,
                gameId = s.gameId,
                authorId = s.authorId,
                screenshotUrl = s.screenshotUrl,
                createdAt = s.createdAt}).ToListAsync();
        }
        public void Add(Screenshot screenshot)
        {
            _context.dbScreenshots.Add(screenshot);
            _context.SaveChanges();
        }
    }
}
