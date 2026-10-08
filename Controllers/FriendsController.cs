using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Profile;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly IFriendsRepository _friendsRepositories;

        public FriendsController(IFriendsRepository friendsRepositories)
        {
            _friendsRepositories = friendsRepositories;
        }

        // Dropped: an unauthenticated "GetAllFriends" used to return every friend
        // relationship for every user in one call. Nothing in the frontend calls it.

        [HttpPost]
        public async Task<ActionResult<Friends>> CreateFriend([FromBody] FriendsModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // A friend relation can only ever be created with the caller as one side;
            // client-supplied model.userId is ignored to prevent creating relations
            // between two other users.
            var result = new Friends(Guid.NewGuid(),
                                            currentUserId.Value,
                                            model.friendId,
                                            DateTime.Now
                                            );
           await _friendsRepositories.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Friends>> GetFriends(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var response = await _friendsRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            if (currentUserId == null || (response.userId != currentUserId.Value && response.friendId != currentUserId.Value))
            {
                return Forbid();
            }

            return Ok(response);
        }

        // Friends lists are shown on every user's public profile page, so (unlike
        // Settings/OwnedGame) this is intentionally not self-only — any authenticated
        // caller may look up another user's friends list. The class-level [Authorize]
        // is what closes the actual leak: fully anonymous scraping.
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
            var currentUserId = GetCurrentUserId();
            var existing = await _friendsRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || (existing.userId != currentUserId.Value && existing.friendId != currentUserId.Value))
            {
                return Forbid();
            }

            await _friendsRepositories.DeleteFriends(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFriends(Guid id, [FromBody] FriendsModel model)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _friendsRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || (existing.userId != currentUserId.Value && existing.friendId != currentUserId.Value))
            {
                return Forbid();
            }

            // userId/friendId are intentionally not taken from the client — the two
            // sides of a friend relation can never be reassigned through this endpoint.
            var result = await _friendsRepositories.UpdateFriends(new Friends(id, existing.userId, existing.friendId, model.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Friends>>> GetAllFriendsByIds([FromBody] List<Guid> guidList)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var response = await _friendsRepositories.GetByUserIds(guidList);

            // Only relations the caller is a party to are ever returned, regardless
            // of which ids were requested.
            return Ok(response.Where(f => f != null && (f.userId == currentUserId.Value || f.friendId == currentUserId.Value)).ToList());
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
