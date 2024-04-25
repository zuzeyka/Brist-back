using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.GameGroupDao
{
    public class GameNewsDao
    {
        private readonly DataContext _context;

        public GameNewsDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameNews>> GetAllGameNews()
        {
            return await _context.dbGameNews
                .Where(g => g.deleteAt == null)
                .Select(g => new GameNews{  id = g.id,
                title = g.title,
                description = g.description,
                likesCount = g.likesCount,
                dislikesCount = g.dislikesCount,
                gameId = g.gameId,
                authorId = g.authorId,
                content = g.content,
                createdAt = g.createdAt}).ToListAsync();
        }
        public void Add(GameNews news)
        {
            _context.dbGameNews.Add(news);
            _context.SaveChanges();
        }

        public async Task DeleteGameNews(Guid id)
        {
            var requirement = await _context.dbGameNews.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameNews> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbGameNews.FirstOrDefault(g => g.id == id));
        }
    }
}
