using FullStackBrist.Server.Models.GameGroup;
using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Data.Entity.Community.GameGroup;
using System.Linq;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupCommentController : Controller
    {       
        private readonly DataContext _dataContext;
        private readonly GroupCommentDao _groupCommentDao;

        public GroupCommentController(DataContext dataContext, GroupCommentDao groupCommentDao)
        {
            _dataContext = dataContext;
            _groupCommentDao = groupCommentDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupCommentDao>>> GetAllGroupComments()
        {
            var _groupComments = await _groupCommentDao.GetAllGroupComments();

            var response = _groupComments.Select(g => new GroupComment(id: g.id,
                                                                       groupId: g.groupId,
                                                                       content: g.content,
                                                                       userId: g.userId,
                                                                       createdAt: g.createdAt)).ToList();
            return Ok(response);
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
            var response = _dataContext.dbGroupComments.AddAsync(result);

            return Ok(response);
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
            var result = new GroupComment(id, group.groupId, group.content, group.userId, group.createdAt);
            await _groupCommentDao.UpdateGroupComment(result);
            return NoContent();
        }
    }
}
