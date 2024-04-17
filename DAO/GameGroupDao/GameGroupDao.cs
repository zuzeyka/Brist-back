using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.GameGroupDao
{
    public class GameGroupDao
    {
        private readonly DataContext _context;

        public GameGroupDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameGroup>> GetAllGameGroups()
        {
            return await _context.dbGameGroups.Select(g => new GameGroup{  id = g.id,
                gameId = g.gameId,
                createdAt = g.createdAt}).ToListAsync();
        }
        public void Add(GameGroup group)
        {
            _context.dbGameGroups.Add(group);
            _context.SaveChanges();
        }
    }
}
