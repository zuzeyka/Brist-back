using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity;

namespace Slush.Repositories
{
    public class DiscussionRepository
    {
        private readonly DataContext _context;
        public DiscussionRepository(DataContext context)
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
                    rate = g.rate,
                    createdAt = g.createdAt
                }).ToListAsync();
        }

        public async Task<Discussion> UpdateDiscussion(Discussion discussion)
        {
            var existing = await _context.dbDiscussions.FindAsync(discussion.id);
            if(existing != null)
            {
                existing.content = discussion.content;
                existing.likesCount = discussion.likesCount;
                existing.rate = discussion.rate;

                await _context.SaveChangesAsync();
            }

            return existing;
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
                    rate = x.rate,
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
                .Where(c => c.deletedAt == null)
                .Select(x => new Discussion
                {
                    id = x.id,
                    authorId = x.authorId,
                    attachedId = x.attachedId,
                    content = x.content,
                    likesCount = x.likesCount,
                    rate = x.rate,
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

        public async Task<List<Discussion?>> GetByIds(List<Guid> id)
        {
            List<Discussion> response = new List<Discussion> ();

            foreach(var item in id)
            {
                var result = await _context.dbDiscussions
                .Where(x => x.id == item)
                .Where(c => c.deletedAt == null)
                .Select(x => new Discussion
                {
                    id = x.id,
                    authorId = x.authorId,
                    attachedId = x.attachedId,
                    content = x.content,
                    likesCount = x.likesCount,
                    rate = x.rate,
                    createdAt = x.createdAt
                }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }
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
