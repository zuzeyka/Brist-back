using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.ProfileRepository
{
    public class UserCommentRepository : IUserCommentRepository
    {
        private readonly DataContext _context;

        public UserCommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserComment>> GetAllUserComments()
        {
            return await _context.dbUserComments
                .Where(s => s.deleteAt == null)
                .Select(s => new UserComment {
                id = s.id,
                userId = s.userId,
                authorId = s.authorId,
                content = s.content,
                createdAt = s.createdAt}).ToListAsync();
        }
        public async Task<UserComment> UpdateUserComment(UserComment category)
        {
            var existing = await _context.dbUserComments.FindAsync(category.id);
            if (existing != null)
            { 
                existing.userId = category.userId;
                existing.authorId = category.authorId;
                existing.content = category.content;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(UserComment comment)
        {
            await _context.dbUserComments.AddAsync(comment);
            _context.SaveChanges();
        }

        public async Task DeleteUserComment(Guid id)
        {
            var requirement = await _context.dbUserComments.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserComment?> GetById(Guid id)
        {
            var response = await _context.dbUserComments
                .Where(x => x.id == id)
                .Select(s => new UserComment
                {
                    id = s.id,
                    userId = s.userId,
                    authorId = s.authorId,
                    content = s.content,
                    createdAt = s.createdAt
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

        public async Task<List<UserComment?>> GetByUId(Guid id)
        {
            var response = await _context.dbUserComments
                .Where(x => x.userId == id)
                .Where(c => c.deleteAt == null)
                .Select(s => new UserComment
                {
                    id = s.id,
                    userId = s.userId,
                    authorId = s.authorId,
                    content = s.content,
                    createdAt = s.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserComment?>> GetByIds(List<Guid> ids)
        {
            List<UserComment> response = new List<UserComment> ();

            foreach(var id in ids)
            {
                var result = await _context.dbUserComments
                    .Where(x => x.id == id)
                    .Where(c => c.deleteAt == null)
                    .Select(s => new UserComment
                    {
                        id = s.id,
                        userId = s.userId,
                        authorId = s.authorId,
                        content = s.content,
                        createdAt = s.createdAt
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
