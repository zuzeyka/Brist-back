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
            return await _context.dbWishedGames.Select(s => new WishedGame {
                id = s.id,
                ownedGameId = s.ownedGameId,
                userId = s.userId,
                createdAt = s.createdAt}).ToListAsync();
        }
        public void Add(WishedGame game)
        {
            _context.dbWishedGames.Add(game);
            _context.SaveChanges();
        }
    }
}
