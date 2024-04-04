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
            var _groupEntities = await _context.dbGroups.AsNoTracking().ToListAsync();

            var _groups = _groupEntities.Select(g => new Group(g.id,
                                                                g.attachedId,
                                                                g.name,
                                                                g.description)).ToList();
            return _groups;
        }

        public void Add(Group group)
        {
            _context.dbGroups.Add(group);
            _context.SaveChanges();
        }
    }
}
