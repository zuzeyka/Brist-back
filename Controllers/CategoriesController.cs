using FullStackBrist.Server.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _CategoriesRepository;

        public CategoriesController(ICategoriesRepository CategoriesRepository)
        {
            _CategoriesRepository = CategoriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ICategoriesRepository>>> GetAllCategories()
        {
            var categories = await _CategoriesRepository.GetAll();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<Categories>> CreateCategories([FromBody] CategoriesModel model)
        {
            var result = new Categories(Guid.NewGuid(),
                                        model.name,
                                        model.description,
                                        DateTime.Now);
            await _CategoriesRepository.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategory(Guid id)
        {
            var response = await _CategoriesRepository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryByAuthor(Guid id)
        {
            await _CategoriesRepository.DeleteCategories(id);
            return NoContent();
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Categories>>> GetAllCategoriesByIds([FromBody] List<Guid> guidList)
        {
            var result = await _CategoriesRepository.GetByIds(guidList);

            return Ok(result);
        }
    }
}
