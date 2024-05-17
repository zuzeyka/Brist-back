using Microsoft.AspNetCore.Mvc;
using Slush.Data;
using Slush.DAO.CategoriesDao;
using Slush.Data.Entity;
using FullStackBrist.Server.Models.Categories;

namespace FullStackBrist.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesForGameController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly CategoryForGameDao _categoryForGameDao;

        public CategoriesForGameController(DataContext dataContext, CategoryForGameDao categoryForGameDao)
        {
            _dataContext = dataContext;
            _categoryForGameDao = categoryForGameDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryForGameDao>>> GetAllCategoriesForGame()
        {
            var categoriesForGame = await _categoryForGameDao.GetAll();

            var response = categoriesForGame.Select(c => new CategoryForGame(id: c.id,
                                                                             gameId: c.gameId,
                                                                             categoryId: c.categoryId,
                                                                             createdAt: c.createdAt)).ToList();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<CategoryForGame>> CreateCategoryForGame([FromBody] CategoryForGameModel model)
        {
            var result = new CategoryForGame(Guid.NewGuid(),
                                        model.gameId,
                                        model.categoryId,
                                        DateTime.Now);
            var response = await _dataContext.dbCategoriesForGame.AddAsync(result);

            return Ok(response);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryForGame(Guid id)
        {
            await _categoryForGameDao.DeleteCategoryForGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryForGame(Guid id, [FromBody] CategoryForGameModel model)
        {
            var result = new CategoryForGame(id, model.gameId, model.categoryId, DateTime.Now);
            await _categoryForGameDao.UpdateCategoryForGame(result);
            return NoContent();
        }
    }
}
