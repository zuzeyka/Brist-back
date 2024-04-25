using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Slush.Data.Entity.Community;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.GroupDao
{
    public class GroupDao
    {
        private readonly DataContext _context;

        public GroupDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _context.dbGroups
                .Where(g => g.deleteAt == null)
                .Select(g => new Group
            {
                id = g.id,
                attachedId = g.attachedId,
                name = g.name,
                description = g.description,
                createdAt = g.createdAt
            }).ToListAsync();

        }
        public void Add(Group group)
        {
            _context.dbGroups.Add(group);
            _context.SaveChanges();
        }

        public async Task DeleteGroup(Guid id)
        {
            var requirement = await _context.dbGroups.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Group> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbGroups.FirstOrDefault(g => g.id == id));
        }
    }
}
