using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

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
            return await _context.dbGamePosts
                .Where(g => g.deleteAt == null)
                .Select(g => new GamePosts {
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
        public async Task UpdateGamePosts(GamePosts post)
        {
            var existing = await _context.dbGamePosts.FindAsync(post.id);
            if (existing != null)
            {
                existing.title = post.title;
                existing.description = post.description;
                existing.likesCount = post.likesCount;
                existing.dislikesCount = post.dislikesCount;
                existing.gameId = post.gameId;
                existing.authorId = post.authorId;
                existing.comments = post.comments;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(GamePosts posts)
        {
            _context.dbGamePosts.Add(posts);
            _context.SaveChanges();
        }

        public async Task DeleteGamePosts(Guid id)
        {
            var requirement = await _context.dbGamePosts.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GamePosts> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbGamePosts.FirstOrDefault(g => g.id == id));
        }
    }
}
