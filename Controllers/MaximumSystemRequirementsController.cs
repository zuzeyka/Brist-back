using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.RequirementsDao;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaximumSystemRequirementsController : Controller
    {
        private readonly MaximumSystemRequirementDao _requirementDao;

        public MaximumSystemRequirementsController(MaximumSystemRequirementDao requirementDao)
        {
            _requirementDao = requirementDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaximumSystemRequirementDao>>> GetAllMaximumSystemRequirements()
        {
            var requirements = await _requirementDao.GetAllMaximumSystemRequirements();

            return Ok(requirements);
        }


        [HttpPost]
        public async Task<ActionResult<MaximumSystemRequirement>> CreateMaximumSystemRequirement([FromBody] MaximumSystemRequirementModel model)
        {
            var result = new MaximumSystemRequirement(Guid.NewGuid(),
                model.gameId,
                model.OS,
                model.processor,
                model.RAM,
                model.video,
                model.freeDiskSpace,
                                            DateTime.Now
                                            );
            await _requirementDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaximumSystemRequirement>> GetMaximumSystemRequirement(Guid id)
        {
            var response = await _requirementDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMaximumSystemRequirement(Guid id)
        {
            await _requirementDao.DeleteMaximumSystemRequirement(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMaximumSystemRequirement(Guid id, [FromBody] MaximumSystemRequirementModel requirement)
        {
            await _requirementDao.UpdateMaximumSystemRequirement(new MaximumSystemRequirement(id, requirement.gameId, requirement.OS, requirement.processor, requirement.RAM, requirement.video, requirement.freeDiskSpace, requirement.createdAt));
            return NoContent();
        }
    }
}
