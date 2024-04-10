using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;
using System.Net.WebSockets;

namespace Slush.DAO.GroupDao
{
    public class GroupCommentDao
    {
        private readonly DataContext _context;

        public GroupCommentDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GroupComment>> GetAllGroupComments()
        {
            var _groupCommentsEntities = await _context.dbGroupComments.AsNoTracking().ToListAsync();

            var _groupComments = _groupCommentsEntities.Select(g => new GroupComment(g.id,
                                                                                     g.groupId,
                                                                                     g.content,
                                                                                     g.userId)).ToList();

            return _groupComments;
        }
        public void Add(GroupComment comment)
        {
            _context.dbGroupComments.Add(comment);
            _context.SaveChanges();
        }
    }
}
