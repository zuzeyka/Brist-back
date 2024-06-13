using FullStackBrist.Server.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CategoriesDao;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly CategoriesDAO _categoriesDao;

        public CategoriesController(CategoriesDAO categoriesDao)
        {
            _categoriesDao = categoriesDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriesDAO>>> GetAllCategories()
        {
            var categories = await _categoriesDao.GetAll();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<Categories>> CreateCategories([FromBody] CategoriesModel model)
        {
            var result = new Categories(Guid.NewGuid(),
                                        model.name,
                                        model.description,
                                        DateTime.Now);
            await _categoriesDao.Add(result);

            return Ok(result);
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
