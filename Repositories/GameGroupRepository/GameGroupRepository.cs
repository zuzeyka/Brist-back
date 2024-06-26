using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.GameGroupRepository
{
    public class GameGroupRepository : IGameGroupRepository
    {
        private readonly DataContext _context;

        public GameGroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameGroup>> GetAllGameGroups()
        {
            return await _context.dbGameGroups
                .Where(g => g.deleteAt == null)
                .Select(g => new GameGroup{  id = g.id,
                gameId = g.gameId,
                createdAt = g.createdAt}).ToListAsync();
        }

        public async Task<GameGroup> UpdateGameGroup(GameGroup group)
        {
            var existing = await _context.dbGameGroups.FindAsync(group.id);
            if (existing != null)
            {
                existing.gameId = group.id;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(GameGroup group)
        {
            await _context.dbGameGroups.AddAsync(group);
            _context.SaveChanges();
        }

        public async Task DeleteGameGroup(Guid id)
        {
            var requirement = await _context.dbGameGroups.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameGroup?> GetById(Guid id)
        {
            var response = await _context.dbGameGroups
                .Where(x => x.id == id)
                .Select(g => new GameGroup
                {
                    id = g.id,
                    gameId = g.gameId,
                    createdAt = g.createdAt
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

        public async Task<List<GameGroup?>> GetByGameId(Guid id)
        {
            var response = await _context.dbGameGroups
                .Where(x => x.gameId == id)
                .Where(c => c.deleteAt == null)
                .Select(g => new GameGroup
                {
                    id = g.id,
                    gameId = g.gameId,
                    createdAt = g.createdAt
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

        public async Task<List<GameGroup?>> GetByIds(List<Guid> id)
        {
            List<GameGroup> response = new List<GameGroup>();

            foreach(var item in id) 
            {
                var result = await _context.dbGameGroups
                .Where(x => x.id == item)
                .Where(c => c.deleteAt == null)
                .Select(g => new GameGroup
                {
                    id = g.id,
                    gameId = g.gameId,
                    createdAt = g.createdAt
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
