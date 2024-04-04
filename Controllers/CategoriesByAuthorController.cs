using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CategoriesDao;
using Slush.Data.Entity;
using Slush.Data;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("categoriesByAuthorController")]
    public class CategoriesByAuthorController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly CategoriesByAuthorDao _categoriesByAuthorDao;

        public CategoriesByAuthorController(DataContext dataContext, CategoriesByAuthorDao categoriesByAuthorDao)
        {
            _dataContext = dataContext;
            _categoriesByAuthorDao = categoriesByAuthorDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriesByAuthorDao>>> GetAllCategoriesByAuthor()
        {
            var _categoriesByAuthor = await _categoriesByAuthorDao.GetAllCategoriesByAuthor();

            var response = _categoriesByAuthor.Select(c => new CategoryByAuthor(c.id,
                                                                                                    c.name,
                                                                                                    c.description,
                                                                                                    c.image)).ToList();

            return Ok(response);
        }
    }
}
