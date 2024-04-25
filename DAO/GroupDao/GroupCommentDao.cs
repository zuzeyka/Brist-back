using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Data.Entity.Community.GameGroup;
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
            return await _context.dbGroupComments
                .Where(g => g.deleteAt == null)
                .Select(g => new GroupComment
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

        public async Task DeleteGroupComment(Guid id)
        {
            var requirement = await _context.dbGroupComments.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GroupComment> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbGroupComments.FirstOrDefault(g => g.id == id));
        }
    }
}
