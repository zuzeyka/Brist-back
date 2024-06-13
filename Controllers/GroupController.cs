using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly GroupDao _groupDao;

        public GroupController(GroupDao groupDao)
        {
            _groupDao = groupDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupDao>>> GetAllGroups()
        {
            var _groups = await _groupDao.GetAllGroups();

            return Ok(_groups);
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
            await _groupDao.Add(result);

            return Ok(result);
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
            await _groupDao.UpdateGroup(new Group(id, group.attachedId, group.name, group.description, group.createdAt));
            return NoContent();
        }
    }
}
