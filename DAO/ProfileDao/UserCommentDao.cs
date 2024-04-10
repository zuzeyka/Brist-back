using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

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
            var _userCommentEntities = await _context.dbUserComments.AsNoTracking().ToListAsync();

            var _userComment = _userCommentEntities.Select(s => new UserComment(s.id,
                                                                                s.userId,
                                                                                s.authorId,
                                                                                s.content)).ToList();

            return _userComment;
        }
        public void Add(UserComment comment)
        {
            _context.dbUserComments.Add(comment);
            _context.SaveChanges();
        }
    }
}
