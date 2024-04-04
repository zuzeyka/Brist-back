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
            var _gameCommentEntities = await _context.dbGameComments.AsNoTracking().ToListAsync();

            var _gameComment = _gameCommentEntities.Select(g => new GameComment(g.id,
                                                                                g.gamePostId,
                                                                                g.content)).ToList(); 
            return _gameComment;
        }

        public void Add(GameComment comment)
        {
            _context.dbGameComments.Add(comment);
            _context.SaveChanges();
        }
    }
}
