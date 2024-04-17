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
            var response = _dataContext.dbCategoriesForGame.AddAsync(result);

            return Ok(response);
        }
    }
}
