using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CreatorsDao;
using Slush.Data;
using Slush.Entity.Store.Product.Creators;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[developerController]")]
    public class DeveloperController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly DeveloperDao _developerDao;

        public DeveloperController(DataContext dataContext, DeveloperDao developerDao)
        {
            _dataContext = dataContext;
            _developerDao = developerDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeveloperDao>>> GetAllDevelopers()
        {
            var _developers = await _developerDao.GetAllDevelopersDao();

            var response = _developers.Select(d => new Publisher(d.id,
                                                                            d.subscribersCount,
                                                                            d.name,
                                                                            d.description,
                                                                            d.avatar,
                                                                            d.backgroundImage,
                                                                            d.urlForNewsPage)).ToList();

            return Ok(response);
        }
    }
}
