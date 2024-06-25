using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;

namespace Slush.DAO.GroupRepository
{
    public class GroupCommentRepository
    {
        private readonly DataContext _context;

        public GroupCommentRepository(DataContext context)
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
        public async Task<GroupComment> UpdateGroupComment(GroupComment comment)
        {
            var existing = await _context.dbGroupComments.FindAsync(comment.id);
            if (existing != null)
            {
                existing.content = comment.content;

                await _context.SaveChangesAsync();
            }

            return existing;
        }
        public async Task Add(GroupComment comment)
        {
            await _context.dbGroupComments.AddAsync(comment);
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

        public async Task<GroupComment?> GetById(Guid id)
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

        public async Task<List<GroupComment?>> GetByIds(List<Guid> id)
        {
            List<GroupComment> response = new List<GroupComment> ();

            foreach(var item in id)
            {
                var result = await _context.dbGroupComments
                .Where(x => x.id == item)
                .Select(g => new GroupComment
                {
                    id = g.id,
                    groupId = g.groupId,
                    content = g.content,
                    userId = g.userId,
                    createdAt = g.createdAt
                }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }
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
