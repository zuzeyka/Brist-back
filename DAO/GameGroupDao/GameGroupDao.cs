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
            var _gameGroupsEntites = await _context.dbGameGroups.AsNoTracking().ToListAsync();

            var _gameGroups = _gameGroupsEntites.Select(g => new GameGroup(g.id,
                                                                           g.gameId)).ToList();

            return _gameGroups;
        }

        public void Add(GameGroup group)
        {
            _context.dbGameGroups.Add(group);
            _context.SaveChanges();
        }
    }
}
