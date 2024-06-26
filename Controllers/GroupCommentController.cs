using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.GroupRepository;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupCommentController : Controller
    {       
        private readonly GroupCommentRepository _groupCommentRepositories;

        public GroupCommentController(GroupCommentRepository groupCommentRepositories)
        {
            _groupCommentRepositories = groupCommentRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupComment>>> GetAllGroupComments()
        {
            var _groupComments = await _groupCommentRepositories.GetAllGroupComments();

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
            await _groupCommentRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupComment>> GetGroupComment(Guid id)
        {
            var response = await _groupCommentRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroupComment(Guid id)
        {
            await _groupCommentRepositories.DeleteGroupComment(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGroupComment(Guid id, [FromBody] GroupCommentModel group)
        {
            var result = await _groupCommentRepositories.UpdateGroupComment(new GroupComment(id, group.groupId, group.content, group.userId, group.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GroupComment>>> GetAllGroupCommentsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _groupCommentRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
