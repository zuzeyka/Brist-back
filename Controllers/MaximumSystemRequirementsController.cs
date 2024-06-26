using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaximumSystemRequirementsController : Controller
    {
        private readonly IMaximumSystemRequirementRepository _requirementRepositories;

        public MaximumSystemRequirementsController(IMaximumSystemRequirementRepository requirementRepositories)
        {
            _requirementRepositories = requirementRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<IMaximumSystemRequirementRepository>>> GetAllMaximumSystemRequirements()
        {
            var requirements = await _requirementRepositories.GetAllMaximumSystemRequirements();

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
            await _requirementRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaximumSystemRequirement>> GetMaximumSystemRequirement(Guid id)
        {
            var response = await _requirementRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<MaximumSystemRequirement>>> GetRequirementsByGameId(Guid id)
        {
            var response = await _requirementRepositories.GetByGameName(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMaximumSystemRequirement(Guid id)
        {
            await _requirementRepositories.DeleteMaximumSystemRequirement(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMaximumSystemRequirement(Guid id, [FromBody] MaximumSystemRequirementModel requirement)
        {
            var result = await _requirementRepositories.UpdateMaximumSystemRequirement(new MaximumSystemRequirement(id, requirement.gameId, requirement.OS, requirement.processor, requirement.RAM, requirement.video, requirement.freeDiskSpace, requirement.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<MaximumSystemRequirement>>> GetAllMaximumSystemRequirementByIds([FromBody] List<Guid> guidList)
        {
            var response = await _requirementRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
