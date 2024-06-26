using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.Repositories.RequirementsRepository
{
    public class MaximumSystemRequirementRepository
    {
        private readonly DataContext _context;

        public MaximumSystemRequirementRepository(DataContext context)
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

        public async Task<MaximumSystemRequirement> UpdateMaximumSystemRequirement(MaximumSystemRequirement requirement)
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

            return existing;
        }

        public async Task Add(MaximumSystemRequirement systemRequirements)
        {
            await _context.dbMaximumSystemRequirements.AddAsync(systemRequirements);
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

        public async Task<MaximumSystemRequirement?> GetById(Guid id)
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

        public async Task<List<MaximumSystemRequirement?>> GetByIds(List<Guid> id)
        {
            List<MaximumSystemRequirement> response = new List<MaximumSystemRequirement> ();

            foreach(var i in id)
            {
                var result = await _context.dbMaximumSystemRequirements
                .Where(x => x.id == i)
                .Where(c => c.deleteAt == null)
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

        public async Task<List<MaximumSystemRequirement?>> GetByGameName(Guid id)
        {
            List<MaximumSystemRequirement> response = new List<MaximumSystemRequirement> ();

                var result = await _context.dbMaximumSystemRequirements
                .Where(x => x.gameId == id)
                .Where(c => c.deleteAt == null)
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

                if(result != null)
                {
                    response.Add(result);
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
