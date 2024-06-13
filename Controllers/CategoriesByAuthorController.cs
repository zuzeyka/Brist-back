using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CategoriesDao;
using Slush.Data.Entity;
using FullStackBrist.Server.Models.Categories;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesByAuthorController : Controller
    {
        private readonly CategoriesByAuthorDao _categoriesByAuthorDao;

        public CategoriesByAuthorController(CategoriesByAuthorDao categoriesByAuthorDao)
        {
            _categoriesByAuthorDao = categoriesByAuthorDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriesByAuthorDao>>> GetAllCategoriesByAuthor()
        {
            var _categoriesByAuthor = await _categoriesByAuthorDao.GetAllCategoriesByAuthor();

            return Ok(_categoriesByAuthor);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryByAuthor>> CreateCategoryByAuthor([FromBody] CategoryByAuthorModel model)
        {
            var result = new CategoryByAuthor(Guid.NewGuid(),
                                        model.authorId,
                                        model.name,
                                        model.description,
                                        model.image,
                                        DateTime.Now);
            await _categoriesByAuthorDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryByAuthor>> GetCategoryByAuthor(Guid id)
        {
            var response = await _categoriesByAuthorDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryByAuthor(Guid id)
        {
            await _categoriesByAuthorDao.DeleteCategoryByAuthor(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryByAuthor(Guid id, [FromBody] CategoryByAuthorModel model)
        {
            await _categoriesByAuthorDao.UpdateCategoriesByAuthor(new CategoryByAuthor(id, model.authorId, model.name, model.description, model.image, DateTime.Now));
            return NoContent();
        }
    }
}
