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
            return await _context.dbOwnedGames.Select(o => new OwnedGame {
                id = o.id,
                ownedGameId = o.ownedGameId,
                userId = o.userId,
                createdAt = o.createdAt}).ToListAsync();
        }
        public void Add(OwnedGame game)
        {
            _context.dbOwnedGames.Add(game);
            _context.SaveChanges();
        }
    }
}
