using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Data.Entity.Profile;
using System.Net.WebSockets;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCommentController : Controller
    {
        private readonly UserCommentRepository _userCommentRepositories;

        public UserCommentController(UserCommentRepository userCommentRepositories)
        {
            _userCommentRepositories = userCommentRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserCommentRepository>>> GetAllUserComments()
        {
            var _userComments = await _userCommentRepositories.GetAllUserComments();

            return Ok(_userComments);
        }

        [HttpPost]
        public async Task<ActionResult<UserComment>> CreateUserComment([FromBody] UserCommentModel model)
        {
            var result = new UserComment(Guid.NewGuid(),
                model.userId,
                model.authorId,
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
        public async Task<ActionResult> DeleteUserComment(Guid id)
        {
            await _userCommentRepositories.DeleteUserComment(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUserComment(Guid id, [FromBody] UserCommentModel comment)
        {
            var result = await _userCommentRepositories.UpdateUserComment(new UserComment(id, comment.userId, comment.authorId, comment.content, comment.createdAt));
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
    }
}
