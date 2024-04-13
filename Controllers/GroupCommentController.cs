using FullStackBrist.Server.Models.GameGroup;
using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Data.Entity.Community.GameGroup;

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

            var response = _groupComments.Select(g => new GroupComment(g.id,
                                                                                     g.groupId,
                                                                                     g.content,
                                                                                     g.userId,
                                                                                     g.createdAt)).ToList();
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
            _dataContext.dbGroupComments.AddAsync(result);

            return result;
        }
    }
}
