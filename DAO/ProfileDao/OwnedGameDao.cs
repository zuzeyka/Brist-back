using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Data.Entity.Community.GameGroup;
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
            return await _context.dbOwnedGames
                .Where(o => o.deleteAt == null)
                .Select(o => new OwnedGame {
                id = o.id,
                ownedGameId = o.ownedGameId,
                userId = o.userId,
                createdAt = o.createdAt}).ToListAsync();
        }
        public async Task UpdateOwnedGame(OwnedGame ownedGame)
        {
            var existing = await _context.dbOwnedGames.FindAsync(ownedGame.id);
            if (existing != null)
            {
                existing.ownedGameId = ownedGame.id;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(OwnedGame game)
        {
            _context.dbOwnedGames.Add(game);
            _context.SaveChanges();
        }

        public async Task DeleteOwnedGame(Guid id)
        {
            var requirement = await _context.dbOwnedGames.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<OwnedGame> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbOwnedGames.FirstOrDefault(o => o.id == id));
        }
    }
}
