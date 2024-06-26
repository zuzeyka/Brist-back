using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.CategoriesRepository;
using Slush.Data.Entity;
using FullStackBrist.Server.Models.Categories;

namespace FullStackBrist.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesForGameController : Controller
    {
        private readonly CategoryForGameRepository _categoryForGameRepositories;

        public CategoriesForGameController(CategoryForGameRepository categoryForGameRepositories)
        {
            _categoryForGameRepositories = categoryForGameRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryForGameRepository>>> GetAllCategoriesForGame()
        {
            var categoriesForGame = await _categoryForGameRepositories.GetAll();

            return Ok(categoriesForGame);
        }


        [HttpPost]
        public async Task<ActionResult<CategoryForGame>> CreateCategoryForGame([FromBody] CategoryForGameModel model)
        {
            var result = new CategoryForGame(Guid.NewGuid(),
                                        model.gameId,
                                        model.categoryId,
                                        DateTime.Now);
            await _categoryForGameRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryForGame>> GetCategoryForGame(Guid id)
        {
            var response = await _categoryForGameRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<CategoryForGame>>> GetCategoryForGameByGameId(Guid id)
        {
            var response = await _categoryForGameRepositories.GetByGameId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryForGame(Guid id)
        {
            await _categoryForGameRepositories.DeleteCategoryForGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryForGame(Guid id, [FromBody] CategoryForGameModel model)
        {
            var result = await _categoryForGameRepositories.UpdateCategoryForGame(new CategoryForGame(id, model.gameId, model.categoryId, DateTime.Now));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<CategoryForGame>>> GetAllCategoriesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _categoryForGameRepositories.GetByGameIds(guidList);

            return Ok(response);
        }
    }
}
