using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Profile;
using Slush.Data.Entity.Community.GameGroup;

namespace Slush.Repositories.RequirementsRepository
{
    public class MinimalSystemRequirementRepository
    {
        private readonly DataContext _context;

        public MinimalSystemRequirementRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MinimalSystemRequirement>> GetAllMinimalSystemRequirements()
        {
            return await _context.dbMinimalSystemRequirements
                .Where(r => r.deleteAt == null)
                .Select(r => new MinimalSystemRequirement {
                id = r.id,
                gameId = r.gameId,
                OS = r.OS,
                processor = r.processor,
                RAM = r.RAM,
                video = r.video,
                freeDiskSpace = r.freeDiskSpace,
                createdAt = r.createdAt}).ToListAsync();
        }

        public async Task<MinimalSystemRequirement> UpdateMinimalSystemRequirement(MinimalSystemRequirement requirement)
        {
            var existing = await _context.dbMinimalSystemRequirements.FindAsync(requirement.id);
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

        public async Task Add(MinimalSystemRequirement systemRequirements)
        {
            await _context.dbMinimalSystemRequirements.AddAsync(systemRequirements);
            _context.SaveChanges();
        }
        public async Task DeleteMinimalSystemRequirement(Guid id)
        {
            var requirement = await _context.dbMinimalSystemRequirements.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<MinimalSystemRequirement?> GetById(Guid id)
        {
            var response = await _context.dbMinimalSystemRequirements
                .Where(x => x.id == id)
                .Select(r => new MinimalSystemRequirement
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

        public async Task<List<MinimalSystemRequirement?>> GetByGameId(Guid id)
        {
            var response = await _context.dbMinimalSystemRequirements
                .Where(x => x.gameId == id)
                .Select(r => new MinimalSystemRequirement
                {
                    id = r.id,
                    gameId = r.gameId,
                    OS = r.OS,
                    processor = r.processor,
                    RAM = r.RAM,
                    video = r.video,
                    freeDiskSpace = r.freeDiskSpace,
                    createdAt = r.createdAt
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

        public async Task<List<MinimalSystemRequirement?>> GetByIds(List<Guid> id)
        {
            List<MinimalSystemRequirement> response = new List<MinimalSystemRequirement> ();

            foreach(var item in id)
            {
                var result = await _context.dbMinimalSystemRequirements
                .Where(x => x.id == item)
                .Select(r => new MinimalSystemRequirement
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
                    response.Add (result);
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
