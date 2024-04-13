using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.GameGroupDao
{
    public class GamePostsDao
    {
        private readonly DataContext _context;

        public GamePostsDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GamePosts>> GetAllGamePosts()
        {
            var _gamePostEntities = await _context.dbGamePosts.AsNoTracking().ToListAsync();

            var _gamePosts = _gamePostEntities.Select(g => new GamePosts(g.id,
                                                                            g.title,
                                                                            g.description,
                                                                            g.likesCount,
                                                                            g.dislikesCount,
                                                                            g.discussionId,
                                                                            g.gameId,
                                                                            g.authorId,
                                                                            g.content, g.createdAt)).ToList();
            return _gamePosts;
        }
        public void Add(GamePosts posts)
        {
            _context.dbGamePosts.Add(posts);
            _context.SaveChanges();
        }
    }
}
