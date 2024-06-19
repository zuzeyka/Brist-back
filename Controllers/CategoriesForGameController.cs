using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CategoriesDao;
using Slush.Data.Entity;
using FullStackBrist.Server.Models.Categories;

namespace FullStackBrist.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesForGameController : Controller
    {
        private readonly CategoryForGameDao _categoryForGameDao;

        public CategoriesForGameController(CategoryForGameDao categoryForGameDao)
        {
            _categoryForGameDao = categoryForGameDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryForGameDao>>> GetAllCategoriesForGame()
        {
            var categoriesForGame = await _categoryForGameDao.GetAll();

            return Ok(categoriesForGame);
        }


        [HttpPost]
        public async Task<ActionResult<CategoryForGame>> CreateCategoryForGame([FromBody] CategoryForGameModel model)
        {
            var result = new CategoryForGame(Guid.NewGuid(),
                                        model.gameId,
                                        model.categoryId,
                                        DateTime.Now);
            await _categoryForGameDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryForGame>> GetCategoryForGame(Guid id)
        {
            var response = await _categoryForGameDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<CategoryForGame>>> GetCategoryForGameByGameId(Guid id)
        {
            var response = await _categoryForGameDao.GetByGameId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryForGame(Guid id)
        {
            await _categoryForGameDao.DeleteCategoryForGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryForGame(Guid id, [FromBody] CategoryForGameModel model)
        {
            var result = await _categoryForGameDao.UpdateCategoryForGame(new CategoryForGame(id, model.gameId, model.categoryId, DateTime.Now));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<CategoryForGame>>> GetAllCategoriesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _categoryForGameDao.GetByGameIds(guidList);

            return Ok(response);
        }
    }
}
