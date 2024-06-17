using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

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
                gameId = g.gameId,
                authorId = g.authorId,
                gameGroupId = g.gameGroupId,
                content = g.content, 
                createdAt = g.createdAt }).ToListAsync();

        }

        public async Task<GameGuide> UpdateGameGuide(GameGuide guide)
        {
            var existing = await _context.dbGameGuides.FindAsync(guide.id);
            if (existing != null)
            {
                existing.title = guide.title;
                existing.description = guide.description;
                existing.likesCount = guide.likesCount;
                existing.gameId = guide.gameId;
                existing.authorId = guide.authorId;
                existing.gameGroupId = guide.gameGroupId;
                existing.content = guide.content;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(GameGuide guide)
        {
            await _context.dbGameGuides.AddAsync(guide);
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

        public async Task<GameGuide?> GetById(Guid id)
        {
            var response = await _context.dbGameGuides
                .Where(x => x.id == id)
                .Select(g => new GameGuide
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
                    gameGroupId = g.gameGroupId,
                    content = g.content,
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
    }
}
