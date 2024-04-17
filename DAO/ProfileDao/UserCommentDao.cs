using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Slush.DAO.ProfileDao
{
    public class UserCommentDao
    {
        private readonly DataContext _context;

        public UserCommentDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserComment>> GetAllUserComments()
        {
            return await _context.dbUserComments.Select(s => new UserComment {
                id = s.id,
                userId = s.userId,
                authorId = s.authorId,
                content = s.content,
                createdAt = s.createdAt}).ToListAsync();
        }
        public void Add(UserComment comment)
        {
            _context.dbUserComments.Add(comment);
            _context.SaveChanges();
        }
    }
}
