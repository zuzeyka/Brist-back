using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
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
                discussionId = p.discussionId,
                authorId = p.authorId,
                content = p.content,
                createdAt = p.createdAt}).ToListAsync();


        }
        public async Task UpdatePost(Post post)
        {
            var existing = await _context.dbPosts.FindAsync(post.id);
            if (existing != null)
            {
                existing.title = post.title;
                existing.description = post.description;
                existing.likesCount = post.likesCount;
                existing.content = post.content;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(Post post)
        {
            await _context.dbPosts.AddAsync(post);
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

        public async Task<Post?> GetById(Guid id)
        {
            var response = await _context.dbPosts
                .Where(x => x.id == id)
                .Select(p => new Post
                {
                    id = p.id,
                    title = p.title,
                    description = p.description,
                    likesCount = p.likesCount,
                    discussionId = p.discussionId,
                    authorId = p.authorId,
                    content = p.content,
                    createdAt = p.createdAt
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
