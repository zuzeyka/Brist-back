using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Slush.Data.Entity.Community;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

namespace Slush.Repositories.GroupRepository
{
    public class GroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
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
                imageUrl = g.imageUrl,
                createdAt = g.createdAt
            }).ToListAsync();

        }
        public async Task<Group> UpdateGroup(Group group)
        {
            var existing = await _context.dbGroups.FindAsync(group.id);
            if (existing != null)
            {
                existing.name = group.name;
                existing.description = group.description;
                existing.imageUrl = group.imageUrl;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(Group group)
        {
            await _context.dbGroups.AddAsync(group);
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

        public async Task<Group?> GetById(Guid id)
        {
            var response = await _context.dbGroups
                .Where(x => x.id == id)
                .Select(g => new Group
                {
                    id = g.id,
                    attachedId = g.attachedId,
                    name = g.name,
                    description = g.description,
                    imageUrl = g.imageUrl,
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

        public async Task<List<Group?>> GetByIds(List<Guid> id)
        {
            List<Group> response = new List<Group> ();

            foreach(var item in id)
            {
                var result = await _context.dbGroups
                .Where(x => x.id == item)
                .Where(c => c.deleteAt == null)
                .Select(g => new Group
                {
                    id = g.id,
                    attachedId = g.attachedId,
                    name = g.name,
                    description = g.description,
                    imageUrl = g.imageUrl,
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
