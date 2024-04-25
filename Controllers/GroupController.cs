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

            var response = _groups.Select(g => new Group(id: g.id,
                                                         attachedId: g.attachedId,
                                                         name: g.name,
                                                         description: g.description,
                                                         createdAt: g.createdAt)).ToList();

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
            var response = _dataContext.dbGroups.AddAsync(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(Guid id)
        {
            var response = await _groupDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(Guid id)
        {
            await _groupDao.DeleteGroup(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGroup(Guid id, [FromBody] GroupModel group)
        {
            var result = new Group(id, group.attachedId, group.name, group.description, group.createdAt);
            await _groupDao.UpdateGroup(result);
            return NoContent();
        }
    }
}
