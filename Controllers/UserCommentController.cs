using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity.Profile;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCommentController : Controller
    {
        // Profile-wall comments are public (like screenshots/topics) — GET endpoints
        // stay anonymous. Only the author can create/edit/delete their own comment.
        private readonly IUserCommentRepository _userCommentRepositories;

        public UserCommentController(IUserCommentRepository userCommentRepositories)
        {
            _userCommentRepositories = userCommentRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserComment>>> GetAllUserComments()
        {
            var _userComments = await _userCommentRepositories.GetAllUserComments();

            return Ok(_userComments);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserComment>> CreateUserComment([FromBody] UserCommentModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // A comment can only ever be posted under the caller's own authorship;
            // client-supplied model.authorId is ignored to prevent impersonating another user.
            var result = new UserComment(Guid.NewGuid(),
                model.userId,
                currentUserId.Value,
                model.content,
                DateTime.Now);

            await _userCommentRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserComment>> GetUserComment(Guid id)
        {
            var response = await _userCommentRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUserComment(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _userCommentRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.authorId != currentUserId.Value)
            {
                return Forbid();
            }

            await _userCommentRepositories.DeleteUserComment(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateUserComment(Guid id, [FromBody] UserCommentModel comment)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _userCommentRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.authorId != currentUserId.Value)
            {
                return Forbid();
            }

            // authorId is intentionally not taken from the client — authorship of a
            // comment can never be reassigned to another user.
            var result = await _userCommentRepositories.UpdateUserComment(new UserComment(id, comment.userId, existing.authorId, comment.content, comment.createdAt));
            return Ok(result);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<UserComment>>> GetAllUserCommentsById(Guid id)
        {
            var response = await _userCommentRepositories.GetByUId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<UserComment>>> GetAllUserCommentsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _userCommentRepositories.GetByIds(guidList);

            return Ok(response);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
