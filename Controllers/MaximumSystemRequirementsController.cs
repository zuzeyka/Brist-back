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
    public class MaximumSystemRequirementsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly MaximumSystemRequirementDao _requirementDao;

        public MaximumSystemRequirementsController(DataContext dataContext, MaximumSystemRequirementDao requirementDao)
        {
            _dataContext = dataContext;
            _requirementDao = requirementDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaximumSystemRequirementDao>>> GetAllMaximumSystemRequirements()
        {
            var requirements = await _requirementDao.GetAllMaximumSystemRequirements();

            var response = requirements.Select(r => new MaximumSystemRequirement(id: r.id,
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
            var response = _dataContext.dbMaximumSystemRequirements.AddAsync(result);

            return Ok(response);
        }
    }
}
