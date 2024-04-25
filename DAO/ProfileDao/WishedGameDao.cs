using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Data.Entity.Community.GameGroup;
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
            return await _context.dbWishedGames
                .Where(s => s.deleteAt == null)
                .Select(s => new WishedGame {
                id = s.id,
                ownedGameId = s.ownedGameId,
                userId = s.userId,
                createdAt = s.createdAt}).ToListAsync();
        }

        public async Task UpdateWishedGame(WishedGame wishedGame)
        {
            var existing = await _context.dbWishedGames.FindAsync(wishedGame.id);
            if (existing != null)
            {
                existing.ownedGameId = wishedGame.id;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(WishedGame game)
        {
            _context.dbWishedGames.Add(game);
            _context.SaveChanges();
        }

        public async Task DeleteWishedGame(Guid id)
        {
            var requirement = await _context.dbWishedGames.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<WishedGame> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbWishedGames.FirstOrDefault(w => w.id == id));
        }
    }
}
