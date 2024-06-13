using FullStackBrist.Server.Models.Creators;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CreatorsDao;
using Slush.Entity.Store.Product.Creators;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : Controller
    {
        private readonly DeveloperDao _developerDao;

        public DeveloperController(DeveloperDao developerDao)
        {
            _developerDao = developerDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeveloperDao>>> GetAllDevelopers()
        {
            var _developers = await _developerDao.GetAllDevelopersDao();

            return Ok(_developers);
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
            await _developerDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(Guid id)
        {
            var response = await _developerDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeveloper(Guid id)
        {
            await _developerDao.DeleteDeveloper(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDeveloper(Guid id, [FromBody] DeveloperModel model)
        {
            await _developerDao.UpdateDeveloper(new Developer(id, model.subscribersCount, model.name, model.description, model.avatar, model.backgroundImage, model.urlForNewsPage, model.createdAt));
            return NoContent();
        }
    }
}
