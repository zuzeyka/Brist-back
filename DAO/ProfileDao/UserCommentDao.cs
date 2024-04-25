using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity;

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
            return await _context.dbUserComments
                .Where(s => s.deleteAt == null)
                .Select(s => new UserComment {
                id = s.id,
                userId = s.userId,
                authorId = s.authorId,
                content = s.content,
                createdAt = s.createdAt}).ToListAsync();
        }
        public async Task UpdateCategoriesByAuthor(CategoryByAuthor category)
        {
            var existing = await _context.dbCategoriesByAuthors.FindAsync(category.id);
            if (existing != null)
            {
                existing.name = category.name;
                existing.description = category.description;
                existing.image = category.image;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(UserComment comment)
        {
            _context.dbUserComments.Add(comment);
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

        public async Task<UserComment> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbUserComments.FirstOrDefault(u => u.id == id));
        }
    }
}
