using Microsoft.AspNetCore.Mvc;
using Slush.DAO.RequirementsDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[minimalSystemRequirementsController]")]
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
                                                                                                r.freeDiskSpace)).ToList();
            return Ok(response);
        }
    }
}
