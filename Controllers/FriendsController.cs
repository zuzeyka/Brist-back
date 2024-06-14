using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendsController : Controller
    {
        private readonly FriendsDao _friendsDao;

        public FriendsController(FriendsDao friendsDao)
        {
            _friendsDao = friendsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<FriendsDao>>> GetAllFriends()
        {
            var _friends = await _friendsDao.GetAllFriends();

            return Ok(_friends);
        }

        [HttpPost]
        public async Task<ActionResult<Friends>> CreateFriend([FromBody] FriendsModel model)
        {
            var result = new Friends(Guid.NewGuid(),
                                            model.userId,
                                            model.friendId,
                                            DateTime.Now
                                            );
           await _friendsDao.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Friends>> GetFriends(Guid id)
        {
            var response = await _friendsDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("getbyuserid/{id}")]
        public async Task<ActionResult<List<Friends>>> GetFriendsByUserId(Guid id)
        {
            var response = await _friendsDao.GetByUserId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFriends(Guid id)
        {
            await _friendsDao.DeleteFriends(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFriends(Guid id, [FromBody] FriendsModel model)
        {
            await _friendsDao.UpdateFriends(new Friends(id, model.userId, model.friendId, model.createdAt));
            return NoContent();
        }
    }
}
