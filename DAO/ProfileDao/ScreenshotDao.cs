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
            var _screenshotsEntities = await _context.dbScreenshots.AsNoTracking().ToListAsync();

            var _screenshots = _screenshotsEntities.Select(s => new Screenshot(s.id,
                                                                                s.title,
                                                                                s.description,
                                                                                s.likesCount,
                                                                                s.dislikesCount,
                                                                                s.discussionId,
                                                                                s.gameId,
                                                                                s.authorId,
                                                                                s.screenshotUrl)).ToList();

            return _screenshots;
        }

        public void Add(Screenshot screenshot)
        {
            _context.dbScreenshots.Add(screenshot);
            _context.SaveChanges();
        }
    }
}
