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
            return await _context.dbGroups.Select(g => new Group
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
    }
}
