using FullStackBrist.Server.Models.Creators;
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

            var response = _developers.Select(d => new Developer(d.id,
                                                                            d.subscribersCount,
                                                                            d.name,
                                                                            d.description,
                                                                            d.avatar,
                                                                            d.backgroundImage,
                                                                            d.urlForNewsPage,
                                                                            d.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Developer>> CreateDeveloper([FromBody] DeveloperModel model)
        {
            var result =  new Developer(Guid.NewGuid(),
                                        0,
                                        model.name,
                                        model.description,
                                        model.avatar,
                                        model.backgroundImage,
                                        null,
                                        DateTime.Now);
            _dataContext.dbDevelopers.AddAsync(result);

            return result;
        }
    }
}
