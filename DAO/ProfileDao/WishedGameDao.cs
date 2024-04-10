using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileDao
{
    public class WishedGameDao
    {
        private readonly DataContext _context;

        public WishedGameDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<WishedGame>> GetAllWishedGames()
        {
            var _wishedGamesEntities = await _context.dbWishedGames.AsNoTracking().ToListAsync();

            var _wishedGames = _wishedGamesEntities.Select(s => new WishedGame(s.id,
                                                                                s.ownedGameId,
                                                                                s.userId)).ToList();

            return _wishedGames;
        }
        public void Add(WishedGame game)
        {
            _context.dbWishedGames.Add(game);
            _context.SaveChanges();
        }
    }
}
