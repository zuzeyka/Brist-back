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
            var _gameNewsEntities = await _context.dbGameNews.AsNoTracking().ToListAsync();

            var _gameNews = _gameNewsEntities.Select(g => new GameNews(g.id,
                                                                        g.title,
                                                                        g.description,
                                                                        g.likesCount,
                                                                        g.dislikesCount,
                                                                        g.gameId,
                                                                        g.authorId,
                                                                        g.content)).ToList();
            return _gameNews;
        }
        public void Add(GameNews news)
        {
            _context.dbGameNews.Add(news);
            _context.SaveChanges();
        }
    }
}
