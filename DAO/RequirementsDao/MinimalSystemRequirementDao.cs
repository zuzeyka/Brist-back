using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

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
            var _requirementsEntities = await _context.dbMinimalSystemRequirements.AsNoTracking().ToListAsync();

            var _requirements = _requirementsEntities.Select(r => new MinimalSystemRequirement(r.id,
                                                                                                r.gameId,
                                                                                                r.OS,
                                                                                                r.processor,
                                                                                                r.RAM,
                                                                                                r.video,
                                                                                                r.freeDiskSpace)).ToList();
            return _requirements;
        }

        public void Add(MinimalSystemRequirement systemRequirements)
        {
            _context.dbMinimalSystemRequirements.Add(systemRequirements);
            _context.SaveChanges();
        }
    }
}
