using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Profile;
using Slush.Data.Entity.Community.GameGroup;

namespace Slush.DAO.RequirementsDao
{
    public class MaximumSystemRequirementDao
    {
        private readonly DataContext _context;

        public MaximumSystemRequirementDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MaximumSystemRequirement>> GetAllMaximumSystemRequirements()
        {
            return await _context.dbMaximumSystemRequirements
                .Where(r => r.deleteAt == null)
                .Select(r => new MaximumSystemRequirement {
                id = r.id,
                gameId = r.gameId,
                OS = r.OS,
                processor = r.processor,
                RAM = r.RAM,
                video = r.video,
                freeDiskSpace = r.freeDiskSpace,
                createdAt = r.createdAt}).ToListAsync();
        }

        public async Task UpdateMaximumSystemRequirement(MaximumSystemRequirement requirement)
        {
            var existing = await _context.dbMaximumSystemRequirements.FindAsync(requirement.id);
            if (existing != null)
            {
                existing.OS = requirement.OS;
                existing.RAM = requirement.RAM;
                existing.processor = requirement.processor;
                existing.video = requirement.video;
                existing.freeDiskSpace = requirement.freeDiskSpace;

                await _context.SaveChangesAsync();
            }
        }

        public void Add(MaximumSystemRequirement systemRequirements)
        {
            _context.dbMaximumSystemRequirements.Add(systemRequirements);
            _context.SaveChanges();
        }

        public async Task DeleteMaximumSystemRequirement(Guid id)
        {
            var requirement = await _context.dbMaximumSystemRequirements.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<MaximumSystemRequirement> GetById(Guid id)
        {
            var response = await _context.dbMaximumSystemRequirements
                .Where(x => x.id == id)
                .Select(r => new MaximumSystemRequirement
                {
                    id = r.id,
                    gameId = r.gameId,
                    OS = r.OS,
                    processor = r.processor,
                    RAM = r.RAM,
                    video = r.video,
                    freeDiskSpace = r.freeDiskSpace,
                    createdAt = r.createdAt
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
