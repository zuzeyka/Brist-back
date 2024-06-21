using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.RequirementsDao;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MinimalSystemRequirementsController : Controller
    {
        private readonly MinimalSystemRequirementDao _minimalSystemRequirementDao;

        public MinimalSystemRequirementsController(MinimalSystemRequirementDao minimalSystemRequirementDao)
        {
            _minimalSystemRequirementDao = minimalSystemRequirementDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<MinimalSystemRequirementDao>>> GetAllMinimalSystemRequirements()
        {
            var requirements = await _minimalSystemRequirementDao.GetAllMinimalSystemRequirements();

            return Ok(requirements);
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
            await _minimalSystemRequirementDao.Add(result);
            return Ok(result);
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

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<MinimalSystemRequirement>> GetRequirementsByGameId(Guid id)
        {
            var response = await _minimalSystemRequirementDao.GetByGameId(id);
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
            var result = await _minimalSystemRequirementDao.UpdateMinimalSystemRequirement(new MinimalSystemRequirement(id, requirement.gameId, requirement.OS, requirement.processor, requirement.RAM, requirement.video, requirement.freeDiskSpace, requirement.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<MinimalSystemRequirement>>> GetMinimalSystemRequirementByIds([FromBody] List<Guid> guidList)
        {
            var response = await _minimalSystemRequirementDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
