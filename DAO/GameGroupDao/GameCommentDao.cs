using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Entity.Store.Product;

namespace Slush.DAO.GameGroupDao
{
    public class GameCommentDao
    {
        private readonly DataContext _context;

        public GameCommentDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameComment>> GetAllGameComments()
        {
            return await _context.dbGameComments
                .Where(g => g.deleteAt == null)
                .Select(g => new GameComment{   id = g.id,
                gamePostId = g.gamePostId,
                content = g.content,
                createdAt = g.createdAt}).ToListAsync();
        }
        public void Add(GameComment comment)
        {
            _context.dbGameComments.Add(comment);
            _context.SaveChanges();
        }

        public async Task DeleteGameComment(Guid id)
        {
            var requirement = await _context.dbGameComments.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameComment> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbGameComments.SingleOrDefault(g => g.id == id));
        }
    }
}
