using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Profile;
using Slush.Models.Profile;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCategoryController : Controller
    {
        private readonly ICategoryByUserForGameRepository _categoryByUserForGameRepositories;
        private readonly IUserCategoryRepository _userCategoryRepositories;
        private readonly IOwnedGameRepository _ownedGameRepositories;
        private List<CategoryByUserForGame>? listOfCategoriesByUser { get; set; }
        private List<UserCategory>? listOfUserCategory { get; set; }
        private List<OwnedGame>? listOwnedGame { get; set; }

        public UserCategoryController(ICategoryByUserForGameRepository categoryByUserForGameRepositories, IUserCategoryRepository userCategoryRepositories, IOwnedGameRepository ownedGameRepositories)
        {
            _categoryByUserForGameRepositories = categoryByUserForGameRepositories;
            _userCategoryRepositories = userCategoryRepositories;
            _ownedGameRepositories = ownedGameRepositories;
        }
        [HttpGet("getcategories")]
        public async Task<ActionResult<List<ICategoryByUserForGameRepository>>> GetAllCategories()
        {
            var categories = await _categoryByUserForGameRepositories.GetAllCategoryByUserForGames();

            listOfCategoriesByUser = categories;

            return Ok(categories);
        }

        [HttpGet("getcategoriesbyuser")]
        public async Task<ActionResult<List<IUserCategoryRepository>>> GetAllCategoriesByUser()
        {
            var categories = await _userCategoryRepositories.GetAllUserCategories();

            listOfUserCategory = categories;

            return Ok(categories);
        }

        [HttpGet("getownedgames")]
        public async Task<ActionResult<List<IOwnedGameRepository>>> GetAllOwnedGames()
        {
            var games = await _ownedGameRepositories.GetAllOwnedGames();

            listOwnedGame = games;

            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<IUserCategoryRepository>>> GetAllCategoriesByGameId(Guid id)
        {
            var categories = await _userCategoryRepositories.GetAllCategoriesByUser(id);

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

            await _categoryByUserForGameRepositories.Add(result);

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

            await _userCategoryRepositories.Add(result);

            return Ok(result);
        }

        [HttpPut("updateusercategories/{id}")]
        public async Task<ActionResult> UpdateUserCategories(Guid id, [FromBody] UserCategoryModel model)
        {
            var result = await _userCategoryRepositories.UpdateUserCategory(new UserCategory(id, model.userId, model.ownedGameId, model.categoryId, model.createdAt));

            return Ok(result);
        }

        [HttpPut("updatecategories/{id}")]
        public async Task<ActionResult> UpdateCategories(Guid id, [FromBody] CategoryByUserForGame model)
        {
            var result = await _categoryByUserForGameRepositories.UpdateCategoryByUserForGame(new CategoryByUserForGame(id, model.name, model.image, model.createdAt));

            return Ok(result);
        }

        [HttpDelete("deletecategories/{id}")]
        public async Task<ActionResult> DeleteCategories(Guid id)
        {
            await _categoryByUserForGameRepositories.Delete(id);
            return NoContent();
        }

        [HttpDelete("deleteusercategoires/{id}")]
        public async Task<ActionResult> DeleteUserCategories(Guid id)
        {
            await _userCategoryRepositories.Delete(id);
            return NoContent();
        }

        [HttpPost("usercategories/getall")]
        public async Task<ActionResult<List<UserCategory>>> GetAllUserCategoriesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _userCategoryRepositories.GetByIds(guidList);

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<CategoryByUserForGame>>> GetAllCategoryByUserForGameByIds([FromBody] List<Guid> guidList)
        {
            var response = await _categoryByUserForGameRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
