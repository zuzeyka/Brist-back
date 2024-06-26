using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Entity.Profile;

namespace Slush.Repositories.ProfileRepository
{
    public class OwnedGameRepository
    {
        private readonly DataContext _context;

        public OwnedGameRepository(DataContext context)
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
        public async Task<OwnedGame> UpdateOwnedGame(OwnedGame ownedGame)
        {
            var existing = await _context.dbOwnedGames.FindAsync(ownedGame.id);
            if (existing != null)
            {
                existing.ownedGameId = ownedGame.id;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(OwnedGame game)
        {
            await _context.dbOwnedGames.AddAsync(game);
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

        public async Task<OwnedGame?> GetById(Guid id)
        {
            var response = await _context.dbOwnedGames
                .Where(x => x.id == id)
                .Select(o => new OwnedGame
                {
                    id = o.id,
                    ownedGameId = o.ownedGameId,
                    userId = o.userId,
                    createdAt = o.createdAt
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
        
        public async Task<List<OwnedGame?>> GetByUId(Guid id)
        {
            var response = await _context.dbOwnedGames
                .Where(x => x.id == id)
                .Where(c => c.deleteAt == null)
                .Select(o => new OwnedGame
                {
                    id = o.id,
                    ownedGameId = o.ownedGameId,
                    userId = o.userId,
                    createdAt = o.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<OwnedGame?> GetByGameId(Guid id, Guid uid)
        {
            var response = await _context.dbOwnedGames
                .Where(x => x.ownedGameId == id)
                .Where(x => x.userId == uid)
                .Select(o => new OwnedGame
                {
                    id = o.id,
                    ownedGameId = o.ownedGameId,
                    userId = o.userId,
                    createdAt = o.createdAt
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<OwnedGame?>> GetByIds(List<Guid> ids)
        {
            List<OwnedGame> response = new List<OwnedGame> ();

            foreach(var id in ids)
            {
                var result = await _context.dbOwnedGames
                .Where(x => x.id == id)
                .Where(c => c.deleteAt == null)
                .Select(o => new OwnedGame
                {
                    id = o.id,
                    ownedGameId = o.ownedGameId,
                    userId = o.userId,
                    createdAt = o.createdAt
                }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
