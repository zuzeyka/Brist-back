using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity;

namespace Slush.DAO
{
    public class DiscussionDao
    {
        private readonly DataContext _context;
        public DiscussionDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Discussion>> GetAllDiscussions()
        {
            return await _context.dbDiscussions
                .Where(g => g.deletedAt == null)
                .Select(g => new Discussion
                {
                    id = g.id,
                    authorId = g.authorId,
                    attachedId = g.attachedId,
                    content = g.content,
                    likesCount = g.likesCount,
                    createdAt = g.createdAt
                }).ToListAsync();
        }

        public async Task UpdateDiscussion(Discussion discussion)
        {
            var existing = await _context.dbDiscussions.FindAsync(discussion.id);
            if(existing != null)
            {
                existing.content = discussion.content;
                existing.likesCount = discussion.likesCount;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(Discussion discussion)
        {
            await _context.dbDiscussions.AddAsync(discussion);
            _context.SaveChanges();
        }

        public async Task DeleteDiscussion(Guid id)
        {
            var response = await _context.dbDiscussions.FindAsync(id);
            if(response != null)
            {
                response.deletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Discussion?> GetById(Guid id)
        {
            var response = await _context.dbDiscussions
                .Where(x => x.id == id)
                .Select(x => new Discussion
                {
                    id = x.id,
                    authorId = x.authorId,
                    attachedId = x.attachedId,
                    content = x.content,
                    likesCount = x.likesCount,
                    createdAt = x.createdAt
                }).FirstOrDefaultAsync();

            if(response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Discussion?>> GetByAttachedId(Guid id)
        {
            var response = await _context.dbDiscussions
                .Where(x => x.attachedId == id)
                .Select(x => new Discussion
                {
                    id = x.id,
                    authorId = x.authorId,
                    attachedId = x.attachedId,
                    content = x.content,
                    likesCount = x.likesCount,
                    createdAt = x.createdAt
                }).ToListAsync();

            if(response != null)
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
