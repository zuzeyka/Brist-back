using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupCommentController : Controller
    {       
        private readonly GroupCommentDao _groupCommentDao;

        public GroupCommentController(GroupCommentDao groupCommentDao)
        {
            _groupCommentDao = groupCommentDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupCommentDao>>> GetAllGroupComments()
        {
            var _groupComments = await _groupCommentDao.GetAllGroupComments();

            return Ok(_groupComments);
        }

        [HttpPost]
        public async Task<ActionResult<GroupComment>> CreateGroupComment([FromBody] GroupCommentModel model)
        {
            var result = new GroupComment(Guid.NewGuid(),
                                            model.groupId,
                                            model.content,
                                            model.userId,
                                            DateTime.Now
                                            );
            await _groupCommentDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupComment>> GetGroupComment(Guid id)
        {
            var response = await _groupCommentDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroupComment(Guid id)
        {
            await _groupCommentDao.DeleteGroupComment(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGroupComment(Guid id, [FromBody] GroupCommentModel group)
        {
            await _groupCommentDao.UpdateGroupComment(new GroupComment(id, group.groupId, group.content, group.userId, group.createdAt));
            return NoContent();
        }
    }
}
