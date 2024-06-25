using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Entity.Profile;
using Slush.Models.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCategoryController : Controller
    {
        private readonly CategoryByUserForGameDao _categoryByUserForGameDao;
        private readonly UserCategoryDao _userCategoryDao;
        private readonly OwnedGameDao _ownedGameDao;
        private List<CategoryByUserForGame>? listOfCategoriesByUser { get; set; }
        private List<UserCategory>? listOfUserCategory { get; set; }
        private List<OwnedGame>? listOwnedGame { get; set; }

        public UserCategoryController(CategoryByUserForGameDao categoryByUserForGameDao, UserCategoryDao userCategoryDao, OwnedGameDao ownedGameDao)
        {
            _categoryByUserForGameDao = categoryByUserForGameDao;
            _userCategoryDao = userCategoryDao;
            _ownedGameDao = ownedGameDao;
        }
        [HttpGet("getcategories")]
        public async Task<ActionResult<List<CategoryByUserForGameDao>>> GetAllCategories()
        {
            var categories = await _categoryByUserForGameDao.GetAllCategoryByUserForGames();

            listOfCategoriesByUser = categories;

            return Ok(categories);
        }

        [HttpGet("getcategoriesbyuser")]
        public async Task<ActionResult<List<UserCategoryDao>>> GetAllCategoriesByUser()
        {
            var categories = await _userCategoryDao.GetAllUserCategories();

            listOfUserCategory = categories;

            return Ok(categories);
        }

        [HttpGet("getownedgames")]
        public async Task<ActionResult<List<OwnedGameDao>>> GetAllOwnedGames()
        {
            var games = await _ownedGameDao.GetAllOwnedGames();

            listOwnedGame = games;

            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserCategoryDao>>> GetAllCategoriesByGameId(Guid id)
        {
            var categories = await _userCategoryDao.GetAllCategoriesByUser(id);

            listOfUserCategory = categories;

            return Ok(categories);
        }

        [HttpPost("category")]
        public async Task<ActionResult<CategoryByUserForGame>> AddCategory([FromBody] CategoryByUserForGameModel model)
        {
            var result = new CategoryByUserForGame(
                Guid.NewGuid(),
                model.name,
                model.image,
                DateTime.Now);

            await _categoryByUserForGameDao.Add(result);

            return Ok(result);
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

            await _userCategoryDao.Add(result);

            return Ok(result);
        }

        [HttpPatch("updateusercategories/{id}")]
        public async Task<ActionResult> UpdateUserCategories(Guid id, [FromBody] UserCategoryModel model)
        {
            var result = await _userCategoryDao.UpdateUserCategory(new UserCategory(id, model.userId, model.ownedGameId, model.categoryId, model.createdAt));

            return Ok(result);
        }

        [HttpPatch("updatecategories/{id}")]
        public async Task<ActionResult> UpdateCategories(Guid id, [FromBody] CategoryByUserForGame model)
        {
            var result = await _categoryByUserForGameDao.UpdateCategoryByUserForGame(new CategoryByUserForGame(id, model.name, model.image, model.createdAt));

            return Ok(result);
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

        [HttpPost("usercategories/getall")]
        public async Task<ActionResult<List<UserCategory>>> GetAllUserCategoriesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _userCategoryDao.GetByIds(guidList);

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<CategoryByUserForGame>>> GetAllCategoryByUserForGameByIds([FromBody] List<Guid> guidList)
        {
            var response = await _categoryByUserForGameDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
