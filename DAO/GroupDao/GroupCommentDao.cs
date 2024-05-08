using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
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
        public async Task UpdateGroupComment(GroupComment comment)
        {
            var existing = await _context.dbGroupComments.FindAsync(comment.id);
            if (existing != null)
            {
                existing.content = comment.content;

                await _context.SaveChangesAsync();
            }
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
            var response = await _context.dbGroupComments
                .Where(x => x.id == id)
                .Select(g => new GroupComment
                {
                    id = g.id,
                    groupId = g.groupId,
                    content = g.content,
                    userId = g.userId,
                    createdAt = g.createdAt
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
