using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Data.Entity.Community.GameGroup;

namespace Slush.DAO.GroupDao
{
    public class PostDao
    {
        private readonly DataContext _context;

        public PostDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await  _context.dbPosts
                .Where(p => p.deleteAt == null)
                .Select(p => new Post {
                id = p.id,
                title = p.title,
                description = p.description,
                likesCount = p.likesCount,
                dislikesCount = p.dislikesCount,
                discussionId = p.discussionId,
                gameId = p.gameId,
                authorId = p.authorId,
                content = p.content,
                createdAt = p.createdAt}).ToListAsync();


        }

        public void Add(Post post)
        {
            _context.dbPosts.Add(post);
            _context.SaveChanges();
        }

        public async Task DeletePost(Guid id)
        {
            var requirement = await _context.dbPosts.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Post> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbPosts.FirstOrDefault(p => p.id == id));
        }
    }
}
