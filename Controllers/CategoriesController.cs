using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using Slush.DAO.CategoriesDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[categoriesController]")]
    public class CategoriesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly CategoriesDAO _categoriesDao;

        public CategoriesController(DataContext dataContext, CategoriesDAO categoriesDao)
        {
            _dataContext = dataContext;
            _categoriesDao = categoriesDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriesDAO>>> GetAllCategories()
        {
            var categories = await _categoriesDao.GetAll();

            var response = categories.Select(c => new Categories(c.id,
                                                                             c.name,
                                                                             c.description
                                                                             )).ToList();
            return Ok(response);
        }
    }
}
