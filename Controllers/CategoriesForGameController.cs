using Microsoft.AspNetCore.Mvc;
using Slush.Data;
using Slush.DAO.CategoriesDao;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{

    [ApiController]
    [Route("[categoriesForGameController]")]
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

            var response = categoriesForGame.Select(c => new CategoryForGame(c.id,
                                                                                            c.gameId,
                                                                                            c.categoryId)).ToList();
            return Ok(response);
        }
    }
}
