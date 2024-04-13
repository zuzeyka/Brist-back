using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                                                                g.description,
                                                                g.createdAt)).ToList();

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup([FromBody] GroupModel model)
        {
            var result = new Group(Guid.NewGuid(),
                model.attachedId,
                model.name,
                model.description,
                                            DateTime.Now
                                            );
            _dataContext.dbGroups.AddAsync(result);

            return result;
        }
    }
}
