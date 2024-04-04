using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

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
            var _requirementsEntities = await _context.dbMaximumSystemRequirements.AsNoTracking().ToListAsync();

            var _requirements = _requirementsEntities.Select(r => new MaximumSystemRequirement(r.id,
                                                                                               r.gameId,
                                                                                               r.OS,
                                                                                               r.processor,
                                                                                               r.RAM,
                                                                                               r.video,
                                                                                               r.freeDiskSpace)).ToList();

            return _requirements;
        }

        public void Add(MaximumSystemRequirement systemRequirements)
        {
            _context.dbMaximumSystemRequirements.Add(systemRequirements);
            _context.SaveChanges();
        }
    }
}
