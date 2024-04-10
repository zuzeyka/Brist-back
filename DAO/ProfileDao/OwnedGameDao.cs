using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileDao
{
    public class OwnedGameDao
    {
        private readonly DataContext _context;

        public OwnedGameDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<OwnedGame>> GetAllOwnedGames()
        {
            var _ownedGamesEntities = await _context.dbOwnedGames.AsNoTracking().ToListAsync();

            var _ownedGames = _ownedGamesEntities.Select(o => new OwnedGame(o.id,
                                                                            o.ownedGameId,
                                                                            o.userId)).ToList();
            return _ownedGames;
        }
        public void Add(OwnedGame game)
        {
            _context.dbOwnedGames.Add(game);
            _context.SaveChanges();
        }
    }
}
