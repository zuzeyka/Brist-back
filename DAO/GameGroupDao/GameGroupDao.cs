using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

namespace Slush.DAO.GameGroupDao
{
    public class GameGroupDao
    {
        private readonly DataContext _context;

        public GameGroupDao(DataContext context)
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

        public async Task UpdateGameGroup(GameGroup group)
        {
            var existing = await _context.dbGameGroups.FindAsync(group.id);
            if (existing != null)
            {
                existing.gameId = group.id;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(GameGroup group)
        {
            _context.dbGameGroups.Add(group);
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

        public async Task<GameGroup> GetById(Guid id)
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
    }
}
