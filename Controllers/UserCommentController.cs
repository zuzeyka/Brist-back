using FullStackBrist.Server.Models.Group;
using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Slush.DAO.GroupDao;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCommentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserCommentDao _userCommentDao;

        public UserCommentController(DataContext dataContext, UserCommentDao userCommentDao)
        {
            _dataContext = dataContext;
            _userCommentDao = userCommentDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserCommentDao>>> GetAllUserComments()
        {
            var _userComments = await _userCommentDao.GetAllUserComments();

            var response = _userComments.Select(s => new UserComment(id: s.id,
                                                                     userId: s.userId,
                                                                     authorId: s.authorId,
                                                                     content: s.content,
                                                                     createdAt: s.createdAt)).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserComment>> CreateUserComment([FromBody] UserCommentModel model)
        {
            var result = new UserComment(Guid.NewGuid(),
                model.userId,
                model.authorId,
                model.content,
                DateTime.Now);

            var response = await _dataContext.dbUserComments.AddAsync(result);

            return Ok(response);
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
            var result = new UserComment(id, comment.userId, comment.authorId, comment.content, comment.createdAt);
            await _userCommentDao.UpdateUserComment(result);
            return NoContent();
        }
    }
}
