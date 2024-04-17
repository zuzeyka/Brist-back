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
            return await _context.dbGameComments.Select(g => new GameComment{   id = g.id,
                gamePostId = g.gamePostId,
                content = g.content,
                createdAt = g.createdAt}).ToListAsync();
        }
        public void Add(GameComment comment)
        {
            _context.dbGameComments.Add(comment);
            _context.SaveChanges();
        }
    }
}
