using FullStackBrist.Server.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using Slush.DAO.CategoriesDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            var response = categories.Select(c => new Categories(id: c.id,
                                                                 name: c.name,
                                                                 description: c.description,
                                                                 createdAt: c.createdAt
                                                                             )).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Categories>> CreateCategories([FromBody] CategoriesModel model)
        {
            var result = new Categories(Guid.NewGuid(),
                                        model.name,
                                        model.description,
                                        DateTime.Now);
            var response = _dataContext.dbCategories.AddAsync(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategory(Guid id)
        {
            var response = await _categoriesDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryByAuthor(Guid id)
        {
            await _categoriesDao.DeleteCategories(id);
            return NoContent();
        }
    }
}
