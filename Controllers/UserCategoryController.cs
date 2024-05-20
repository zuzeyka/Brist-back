using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Entity.Profile;
using Slush.Models.Profile;
using System.Runtime.CompilerServices;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCategoryController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly CategoryByUserForGameDao _categoryByUserForGameDao;
        private readonly UserCategoryDao _userCategoryDao;
        private readonly OwnedGameDao _ownedGameDao;
        private List<CategoryByUserForGame>? listOfCategoriesByUser { get; set; }
        private List<UserCategory>? listOfUserCategory { get; set; }
        private List<OwnedGame>? listOwnedGame { get; set; }

        public UserCategoryController(DataContext dataContext, CategoryByUserForGameDao categoryByUserForGameDao, UserCategoryDao userCategoryDao, OwnedGameDao ownedGameDao)
        {
            _dataContext = dataContext;
            _categoryByUserForGameDao = categoryByUserForGameDao;
            _userCategoryDao = userCategoryDao;
            _ownedGameDao = ownedGameDao;
        }
        [HttpGet("getcategories")]
        public async Task<ActionResult<List<CategoryByUserForGameDao>>> GetAllCategories()
        {
            var categories = await _categoryByUserForGameDao.GetAllCategoryByUserForGames();

            var response = categories.Select(c => new CategoryByUserForGame(
                id: c.id,
                name: c.name,
                image: c.image,
                createdAt: c.createdAt
                )).ToList();

            listOfCategoriesByUser = response;

            return Ok(response);
        }

        [HttpGet("getcategoriesbyuser")]
        public async Task<ActionResult<List<UserCategoryDao>>> GetAllCategoriesByUser()
        {
            var categories = await _userCategoryDao.GetAllUserCategories();

            var response = categories.Select(u => new UserCategory(
                id: u.id,
                userId: u.userId,
                ownedGameId: u.ownedGameId,
                categoryId: u.categoryId,
                createdAt: u.createdAt
                )).ToList();

            listOfUserCategory = response;

            return Ok(response);
        }

        [HttpGet("getownedgames")]
        public async Task<ActionResult<List<OwnedGameDao>>> GetAllOwnedGames()
        {
            var games = await _ownedGameDao.GetAllOwnedGames();

            var response = games.Select(g => new OwnedGame(
                id: g.id,
                ownedGameId: g.ownedGameId,
                userId: g.userId,
                createdAt: g.createdAt
                )).ToList();

            listOwnedGame = response;

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserCategoryDao>>> GetAllCategoriesByGameId(Guid id)
        {
            var categories = await _userCategoryDao.GetAllCategoriesByUser(id);

            var response = categories.Select(u => new UserCategory(
                id: u.id,
                userId: u.userId,
                ownedGameId: u.ownedGameId,
                categoryId: u.categoryId,
                createdAt: u.createdAt
                )).ToList();

            listOfUserCategory = response;

            return Ok(response);
        }

        [HttpPost("category")]
        public async Task<ActionResult<CategoryByUserForGame>> AddCategory([FromBody] CategoryByUserForGameModel model)
        {
            var result = new CategoryByUserForGame(
                Guid.NewGuid(),
                model.name,
                model.image,
                DateTime.Now);

            var response = await _dataContext.dbCategoryByUserForGames.AddAsync(result);

            return Ok(response);
        }

        [HttpPost("usercategory")]
        public async Task<ActionResult<UserCategory>> AddUserCategory([FromBody] UserCategoryModel model)
        {
            var result = new UserCategory(
                Guid.NewGuid(),
                model.userId,
                model.ownedGameId,
                model.categoryId,
                DateTime.Now);

            var response = await _dataContext.dbUserCategories.AddAsync(result);

            return Ok(response);
        }

        [HttpPut("updateusercategories/{id}")]
        public async Task<ActionResult> UpdateUserCategories(Guid id, [FromBody] UserCategoryModel model)
        {
            var result = new UserCategory(id, model.userId, model.ownedGameId, model.categoryId, model.createdAt);
            await _userCategoryDao.UpdateUserCategory(result);

            return NoContent();
        }

        [HttpPut("updatecategories/{id}")]
        public async Task<ActionResult> UpdateCategories(Guid id, [FromBody] CategoryByUserForGame model)
        {
            var result = new CategoryByUserForGame(id, model.name, model.image, model.createdAt);
            await _categoryByUserForGameDao.UpdateCategoryByUserForGame(result);

            return NoContent();
        }

        [HttpDelete("deletecategories/{id}")]
        public async Task<ActionResult> DeleteCategories(Guid id)
        {
            await _categoryByUserForGameDao.Delete(id);
            return NoContent();
        }

        [HttpDelete("deleteusercategoires/{id}")]
        public async Task<ActionResult> DeleteUserCategories(Guid id)
        {
            await _userCategoryDao.Delete(id);
            return NoContent();
        }
    }
}
