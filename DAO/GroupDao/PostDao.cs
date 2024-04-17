using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;

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
            return await  _context.dbPosts.Select(p => new Post {
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
    }
}
