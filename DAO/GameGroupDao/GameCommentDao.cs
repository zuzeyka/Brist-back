using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
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
        public async Task UpdateGameComment(GameComment comment)
        {
            var existing = await _context.dbGameComments.FindAsync(comment.id);
            if (existing != null)
            {
                existing.gamePostId = comment.id;
                existing.content = comment.content;

                await _context.SaveChangesAsync();
            }
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
            var response = await _context.dbGameComments
                .Where(x => x.id == id)
                .Select(g => new GameComment
                {
                    id = g.id,
                    gamePostId = g.gamePostId,
                    content = g.content,
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
