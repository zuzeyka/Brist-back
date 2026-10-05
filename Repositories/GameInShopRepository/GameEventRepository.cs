using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Store.Product;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.GameInShopRepository
{
    public class GameEventRepository : IGameEventRepository
    {
        private readonly DataContext _context;

        public GameEventRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameEvent>> GetAll()
        {
            return await _context.dbGameEvents
                .Where(e => e.deleteAt == null)
                .Select(e => new GameEvent
                {
                    id = e.id,
                    name = e.name,
                    description = e.description,
                    startAt = e.startAt,
                    endAt = e.endAt,
                    createdAt = e.createdAt
                }).ToListAsync();
        }

        public async Task<GameEvent?> GetById(Guid id)
        {
            return await _context.dbGameEvents
                .Where(e => e.id == id)
                .Where(e => e.deleteAt == null)
                .Select(e => new GameEvent
                {
                    id = e.id,
                    name = e.name,
                    description = e.description,
                    startAt = e.startAt,
                    endAt = e.endAt,
                    createdAt = e.createdAt
                }).FirstOrDefaultAsync();
        }

        public async Task<GameEvent> UpdateGameEvent(GameEvent gameEvent)
        {
            var existing = await _context.dbGameEvents.FindAsync(gameEvent.id);
            if (existing != null)
            {
                existing.name = gameEvent.name;
                existing.description = gameEvent.description;
                existing.startAt = gameEvent.startAt;
                existing.endAt = gameEvent.endAt;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(GameEvent gameEvent)
        {
            await _context.dbGameEvents.AddAsync(gameEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameEvent(Guid id)
        {
            var existing = await _context.dbGameEvents.FindAsync(id);
            if (existing != null)
            {
                existing.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
