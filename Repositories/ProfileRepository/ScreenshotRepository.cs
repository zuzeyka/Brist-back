using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity.Profile;
using Slush.Entity.Profile;
using System.Collections.Immutable;

namespace Slush.DAO.ProfileRepository
{
    public class ScreenshotRepository
    {
        private readonly DataContext _context;

        public ScreenshotRepository(DataContext context)
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
                gameId = s.gameId,
                authorId = s.authorId,
                contentUrl = s.contentUrl,
                createdAt = s.createdAt}).ToListAsync();
        }

        public async Task<Screenshot> UpdateScreenshot(Screenshot screenshot)
        {
            var existing = await _context.dbScreenshots.FindAsync(screenshot.id);
            if (existing != null)
            {
                existing.title = screenshot.title;
                existing.description = screenshot.description;
                existing.likesCount = screenshot.likesCount;
                existing.contentUrl = screenshot.contentUrl;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(Screenshot screenshot)
        {
            await _context.dbScreenshots.AddAsync(screenshot);
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

        public async Task<Screenshot?> GetById(Guid id)
        {
            var response = await _context.dbScreenshots
                .Where(x => x.id == id)
                .Select(s => new Screenshot
                {
                    id = s.id,
                    title = s.title,
                    description = s.description,
                    likesCount = s.likesCount,
                    gameId = s.gameId,
                    authorId = s.authorId,
                    contentUrl = s.contentUrl,
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
        public async Task<List<Screenshot?>> GetByGameId(Guid id)
        {
            var response = await _context.dbScreenshots
                .Where(x => x.gameId == id)
                .Select(s => new Screenshot
                {
                    id = s.id,
                    title = s.title,
                    description = s.description,
                    likesCount = s.likesCount,
                    gameId = s.gameId,
                    authorId = s.authorId,
                    contentUrl = s.contentUrl,
                    createdAt = s.createdAt
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

        public async Task<List<Screenshot?>> GetByUserId(Guid id)
        {
            var response = await _context.dbScreenshots
                .Where(x => x.authorId == id)
                .Select(s => new Screenshot
                {
                    id = s.id,
                    title = s.title,
                    description = s.description,
                    likesCount = s.likesCount,
                    gameId = s.gameId,
                    authorId = s.authorId,
                    contentUrl = s.contentUrl,
                    createdAt = s.createdAt
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

        public async Task<List<Screenshot?>> GetByIds(List<Guid> ids)
        {
            List<Screenshot> response = new List<Screenshot>();

            foreach(var id in ids)
            {
                var result = await _context.dbScreenshots
                .Where(x => x.gameId == id)
                .Select(s => new Screenshot
                {
                    id = s.id,
                    title = s.title,
                    description = s.description,
                    likesCount = s.likesCount,
                    gameId = s.gameId,
                    authorId = s.authorId,
                    contentUrl = s.contentUrl,
                    createdAt = s.createdAt
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
