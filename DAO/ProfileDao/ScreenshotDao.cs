using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Data.Entity.Community.GameGroup;
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
            return await _context.dbScreenshots
                .Where(s => s.deleteAt == null)
                .Select(s => new Screenshot {
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

        public async Task UpdateScreenshot(Screenshot screenshot)
        {
            var existing = await _context.dbScreenshots.FindAsync(screenshot.id);
            if (existing != null)
            {
                existing.title = screenshot.title;
                existing.description = screenshot.description;
                existing.likesCount = screenshot.likesCount;
                existing.dislikesCount = screenshot.dislikesCount;
                existing.discussionId = screenshot.discussionId;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(Screenshot screenshot)
        {
            _context.dbScreenshots.Add(screenshot);
            _context.SaveChanges();
        }

        public async Task DeleteScreenshot(Guid id)
        {
            var requirement = await _context.dbScreenshots.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Screenshot> GetById(Guid id)
        {
            var response = await _context.dbScreenshots
                .Where(x => x.id == id)
                .Select(s => new Screenshot
                {
                    id = s.id,
                    title = s.title,
                    description = s.description,
                    likesCount = s.likesCount,
                    dislikesCount = s.dislikesCount,
                    discussionId = s.discussionId,
                    gameId = s.gameId,
                    authorId = s.authorId,
                    screenshotUrl = s.screenshotUrl,
                    createdAt = s.createdAt
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
