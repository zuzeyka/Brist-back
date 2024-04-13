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
            var _postsEntity = await  _context.dbPosts.AsNoTracking().ToListAsync();

            var _posts = _postsEntity.Select(p => new Post(p.id,
                                                           p.title,
                                                           p.description,
                                                           p.likesCount,
                                                           p.dislikesCount,
                                                           p.discussionId,
                                                           p.gameId,
                                                           p.authorId,
                                                           p.content, p.createdAt)).ToList();

            return _posts;
        }

        public void Add(Post post)
        {
            _context.dbPosts.Add(post);
            _context.SaveChanges();
        }
    }
}
