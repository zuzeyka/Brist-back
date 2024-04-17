using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Profile;

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
            return await _context.dbMaximumSystemRequirements.Select(r => new MaximumSystemRequirement {
                id = r.id,
                gameId = r.gameId,
                OS = r.OS,
                processor = r.processor,
                RAM = r.RAM,
                video = r.video,
                freeDiskSpace = r.freeDiskSpace,
                createdAt = r.createdAt}).ToListAsync();
        }

        public void Add(MaximumSystemRequirement systemRequirements)
        {
            _context.dbMaximumSystemRequirements.Add(systemRequirements);
            _context.SaveChanges();
        }
    }
}
