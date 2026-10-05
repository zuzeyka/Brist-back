using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Store.Product;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.GameInShopRepository
{
    public class GameEventForGameRepository : IGameEventForGameRepository
    {
        private readonly DataContext _context;

        public GameEventForGameRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameEventForGame>> GetAll()
        {
            return await _context.dbGameEventsForGame
                .Where(l => l.deleteAt == null)
                .Select(l => new GameEventForGame
                {
                    id = l.id,
                    gameId = l.gameId,
                    eventId = l.eventId,
                    createdAt = l.createdAt
                }).ToListAsync();
        }

        public async Task<GameEventForGame?> GetById(Guid id)
        {
            return await _context.dbGameEventsForGame
                .Where(l => l.id == id)
                .Where(l => l.deleteAt == null)
                .Select(l => new GameEventForGame
                {
                    id = l.id,
                    gameId = l.gameId,
                    eventId = l.eventId,
                    createdAt = l.createdAt
                }).FirstOrDefaultAsync();
        }

        public async Task<List<GameEventForGame>> GetByGameId(Guid gameId)
        {
            return await _context.dbGameEventsForGame
                .Where(l => l.gameId == gameId)
                .Where(l => l.deleteAt == null)
                .Select(l => new GameEventForGame
                {
                    id = l.id,
                    gameId = l.gameId,
                    eventId = l.eventId,
                    createdAt = l.createdAt
                }).ToListAsync();
        }

        public async Task Add(GameEventForGame link)
        {
            await _context.dbGameEventsForGame.AddAsync(link);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameEventForGame(Guid id)
        {
            var existing = await _context.dbGameEventsForGame.FindAsync(id);
            if (existing != null)
            {
                existing.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
