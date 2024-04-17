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
            return await _context.dbGamePosts.Select(g => new GamePosts {
                id = g.id,
                title = g.title,
                description = g.description,
                likesCount = g.likesCount,
                dislikesCount = g.dislikesCount,
                discussionId = g.discussionId,
                gameId = g.gameId,
                authorId = g.authorId,
                content = g.content,
                createdAt = g.createdAt}).ToListAsync();
        }
        public void Add(GamePosts posts)
        {
            _context.dbGamePosts.Add(posts);
            _context.SaveChanges();
        }
    }
}
