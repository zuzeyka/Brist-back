using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCommentController : Controller
    {
        private readonly UserCommentDao _userCommentDao;

        public UserCommentController(UserCommentDao userCommentDao)
        {
            _userCommentDao = userCommentDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserCommentDao>>> GetAllUserComments()
        {
            var _userComments = await _userCommentDao.GetAllUserComments();

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

            await _userCommentDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserComment>> GetUserComment(Guid id)
        {
            var response = await _userCommentDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserComment(Guid id)
        {
            await _userCommentDao.DeleteUserComment(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserComment(Guid id, [FromBody] UserCommentModel comment)
        {
            await _userCommentDao.UpdateUserComment(new UserComment(id, comment.userId, comment.authorId, comment.content, comment.createdAt));
            return NoContent();
        }
    }
}
