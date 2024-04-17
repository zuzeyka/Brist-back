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
            return await _context.dbGameNews.Select(g => new GameNews{  id = g.id,
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
    }
}
