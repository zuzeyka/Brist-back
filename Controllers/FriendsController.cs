using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendsController : Controller
    {
        private readonly FriendsRepository _friendsRepositories;

        public FriendsController(FriendsRepository friendsRepositories)
        {
            _friendsRepositories = friendsRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<FriendsRepository>>> GetAllFriends()
        {
            var _friends = await _friendsRepositories.GetAllFriends();

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
           await _friendsRepositories.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Friends>> GetFriends(Guid id)
        {
            var response = await _friendsRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("getbyuserid/{id}")]
        public async Task<ActionResult<List<Friends>>> GetFriendsByUserId(Guid id)
        {
            var response = await _friendsRepositories.GetByUserId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFriends(Guid id)
        {
            await _friendsRepositories.DeleteFriends(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFriends(Guid id, [FromBody] FriendsModel model)
        {
            var result = await _friendsRepositories.UpdateFriends(new Friends(id, model.userId, model.friendId, model.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Friends>>> GetAllFriendsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _friendsRepositories.GetByUserIds(guidList);

            return Ok(response);
        }
    }
}
