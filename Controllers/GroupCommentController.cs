using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[groupCommentController]")]
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
                                                                                     g.userId)).ToList();
            return Ok(response);
        }
    }
}
