using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[groupController]")]
    public class GroupController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GroupDao _groupDao;

        public GroupController(DataContext dataContext, GroupDao groupDao)
        {
            _dataContext = dataContext;
            _groupDao = groupDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupDao>>> GetAllGroups()
        {
            var _groups = await _groupDao.GetAllGroups();

            var response = _groups.Select(g => new Group(g.id,
                                                                g.attachedId,
                                                                g.name,
                                                                g.description)).ToList();

            return Ok(response);
        }
    }
}
