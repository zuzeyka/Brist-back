using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;
using System.Linq;
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
            return await _context.dbGroupComments.Select(g => new GroupComment
            {
                id = g.id,
                groupId = g.groupId,
                content = g.content,
                userId = g.userId,
                createdAt = g.createdAt
            }).ToListAsync();


        }
        public void Add(GroupComment comment)
        {
            _context.dbGroupComments.Add(comment);
            _context.SaveChanges();
        }
    }
}
