using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Profile;

namespace Slush.DAO.RequirementsDao
{
    public class MinimalSystemRequirementDao
    {
        private readonly DataContext _context;

        public MinimalSystemRequirementDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MinimalSystemRequirement>> GetAllMinimalSystemRequirements()
        {
            return await _context.dbMinimalSystemRequirements.Select(r => new MinimalSystemRequirement {
                id = r.id,
                gameId = r.gameId,
                OS = r.OS,
                processor = r.processor,
                RAM = r.RAM,
                video = r.video,
                freeDiskSpace = r.freeDiskSpace,
                createdAt = r.createdAt}).ToListAsync();
        }

        public void Add(MinimalSystemRequirement systemRequirements)
        {
            _context.dbMinimalSystemRequirements.Add(systemRequirements);
            _context.SaveChanges();
        }
    }
}
