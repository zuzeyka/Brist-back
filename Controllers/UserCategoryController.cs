using Microsoft.AspNetCore.Authorization;
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
        public UserCategoryController(ICategoryByUserForGameRepository categoryByUserForGameRepositories, IUserCategoryRepository userCategoryRepositories)
        {
            _categoryByUserForGameRepositories = categoryByUserForGameRepositories;
            _userCategoryRepositories = userCategoryRepositories;
        }
        [HttpGet("getcategories")]
        public async Task<ActionResult<List<ICategoryByUserForGameRepository>>> GetAllCategories()
        {
            var categories = await _categoryByUserForGameRepositories.GetAllCategoryByUserForGames();

            return Ok(categories);
        }

        // Dropped: "getcategoriesbyuser" and "getownedgames" used to return every user's
        // category assignments / owned games with no auth check at all. Neither is
        // called from the frontend (OwnedGameController.GetAllOwnedGames already serves
        // the self-only version of the latter) — removed rather than guarded since
        // there's no legitimate caller to keep working.

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<IUserCategoryRepository>>> GetAllCategoriesByGameId(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null || id != currentUserId.Value)
            {
                return Forbid();
            }

            var categories = await _userCategoryRepositories.GetAllCategoriesByUser(id);

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
        [Authorize]
        public async Task<ActionResult<UserCategory>> AddUserCategory([FromBody] UserCategoryModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // A category assignment can only ever be created for the caller themselves;
            // client-supplied model.userId is ignored to prevent tagging another user's game.
            var result = new UserCategory(
                Guid.NewGuid(),
                currentUserId.Value,
                model.ownedGameId,
                model.categoryId,
                DateTime.Now);

            await _userCategoryRepositories.Add(result);

            return Ok(result);
        }

        [HttpPut("updateusercategories/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateUserCategories(Guid id, [FromBody] UserCategoryModel model)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _userCategoryRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            // userId is intentionally not taken from the client — ownership of a
            // category assignment can never be reassigned to another user.
            var result = await _userCategoryRepositories.UpdateUserCategory(new UserCategory(id, existing.userId, model.ownedGameId, model.categoryId, model.createdAt));

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
        [Authorize]
        public async Task<ActionResult> DeleteUserCategories(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _userCategoryRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            await _userCategoryRepositories.Delete(id);
            return NoContent();
        }

        [HttpPost("usercategories/getall")]
        [Authorize]
        public async Task<ActionResult<List<UserCategory>>> GetAllUserCategoriesByIds([FromBody] List<Guid> guidList)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var response = await _userCategoryRepositories.GetByIds(guidList);

            // Only the caller's own category assignments are ever returned, regardless
            // of which ids were requested.
            return Ok(response.Where(c => c != null && c.userId == currentUserId.Value).ToList());
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<CategoryByUserForGame>>> GetAllCategoryByUserForGameByIds([FromBody] List<Guid> guidList)
        {
            var response = await _categoryByUserForGameRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
