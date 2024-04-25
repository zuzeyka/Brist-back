using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.RequirementsDao;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MinimalSystemRequirementsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly MinimalSystemRequirementDao _minimalSystemRequirementDao;

        public MinimalSystemRequirementsController(DataContext dataContext, MinimalSystemRequirementDao minimalSystemRequirementDao)
        {
            _dataContext = dataContext;
            _minimalSystemRequirementDao = minimalSystemRequirementDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<MinimalSystemRequirementDao>>> GetAllMinimalSystemRequirements()
        {
            var requirements = await _minimalSystemRequirementDao.GetAllMinimalSystemRequirements();

            var response = requirements.Select(r => new MinimalSystemRequirement(id: r.id,
                                                                                 gameId: r.gameId,
                                                                                 oS: r.OS,
                                                                                 processor: r.processor,
                                                                                 rAM: r.RAM,
                                                                                 video: r.video,
                                                                                 freeDiskSpace: r.freeDiskSpace,
                                                                                 createdAt: r.createdAt)).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<MinimalSystemRequirement>> CreateMinimalSystemRequirementDao([FromBody] MinimalSystemRequirementModel model)
        {
            var result = new MinimalSystemRequirement(Guid.NewGuid(),
                model.gameId,
                model.OS,
                model.processor,
                model.RAM,
                model.video,
                model.freeDiskSpace,
                                            DateTime.Now
                                            );
            var response = _dataContext.dbMinimalSystemRequirements.AddAsync(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MinimalSystemRequirement>> GetMinimalSystemRequirement(Guid id)
        {
            var response = await _minimalSystemRequirementDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMinimalSystemRequirement(Guid id)
        {
            await _minimalSystemRequirementDao.DeleteMinimalSystemRequirement(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMinimalSystemRequirement(Guid id, [FromBody] MinimalSystemRequirementModel requirement)
        {
            var result = new MinimalSystemRequirement(id, requirement.gameId, requirement.OS, requirement.processor, requirement.RAM, requirement.video, requirement.freeDiskSpace, requirement.createdAt);
            await _minimalSystemRequirementDao.UpdateMinimalSystemRequirement(result);
            return NoContent();
        }
    }
}
