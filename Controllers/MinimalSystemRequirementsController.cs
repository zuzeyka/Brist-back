using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.RequirementsDao;
using Slush.Data;
using Slush.Data.Entity;

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

            var response = requirements.Select(r => new MinimalSystemRequirement(r.id,
                                                                                                r.gameId,
                                                                                                r.OS,
                                                                                                r.processor,
                                                                                                r.RAM,
                                                                                                r.video,
                                                                                                r.freeDiskSpace,
                                                                                                r.createdAt)).ToList();
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
            _dataContext.dbMinimalSystemRequirements.AddAsync(result);
            return Ok(result);
        }
    }
}
