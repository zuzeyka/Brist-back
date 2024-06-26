using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MinimalSystemRequirementsController : Controller
    {
        private readonly IMinimalSystemRequirementRepository _minimalSystemRequirementRepositories;

        public MinimalSystemRequirementsController(IMinimalSystemRequirementRepository minimalSystemRequirementRepositories)
        {
            _minimalSystemRequirementRepositories = minimalSystemRequirementRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<IMinimalSystemRequirementRepository>>> GetAllMinimalSystemRequirements()
        {
            var requirements = await _minimalSystemRequirementRepositories.GetAllMinimalSystemRequirements();

            return Ok(requirements);
        }

        [HttpPost]
        public async Task<ActionResult<MinimalSystemRequirement>> CreateMinimalSystemRequirementRepositories([FromBody] MinimalSystemRequirementModel model)
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
            await _minimalSystemRequirementRepositories.Add(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MinimalSystemRequirement>> GetMinimalSystemRequirement(Guid id)
        {
            var response = await _minimalSystemRequirementRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<MinimalSystemRequirement>> GetRequirementsByGameId(Guid id)
        {
            var response = await _minimalSystemRequirementRepositories.GetByGameId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMinimalSystemRequirement(Guid id)
        {
            await _minimalSystemRequirementRepositories.DeleteMinimalSystemRequirement(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMinimalSystemRequirement(Guid id, [FromBody] MinimalSystemRequirementModel requirement)
        {
            var result = await _minimalSystemRequirementRepositories.UpdateMinimalSystemRequirement(new MinimalSystemRequirement(id, requirement.gameId, requirement.OS, requirement.processor, requirement.RAM, requirement.video, requirement.freeDiskSpace, requirement.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<MinimalSystemRequirement>>> GetMinimalSystemRequirementByIds([FromBody] List<Guid> guidList)
        {
            var response = await _minimalSystemRequirementRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
