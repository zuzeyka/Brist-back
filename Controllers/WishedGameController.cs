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
    public class WishedGameController : Controller
    {
        private readonly IWishedGameRepository _wishedGameRepositories;

        public WishedGameController(IWishedGameRepository wishedGameRepositories)
        {
            _wishedGameRepositories = wishedGameRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<WishedGame>>> GetAllWishedGames()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var wishedGames = await _wishedGameRepositories.GetAllWishedGames();

            return Ok(wishedGames.Where(w => w.userId == currentUserId.Value).ToList());
        }


        [HttpPost]
        public async Task<ActionResult<WishedGame>> CreateWishedGame([FromBody] WishedGameModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // A game can only ever be wished for the caller themselves; client-supplied
            // model.userId is ignored to prevent wishing a game on another user's behalf.
            var result = new WishedGame(Guid.NewGuid(),
                model.ownedGameId,
                currentUserId.Value,
                DateTime.Now);

            await _wishedGameRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WishedGame>> GetWishedGame(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var response = await _wishedGameRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            if (currentUserId == null || response.userId != currentUserId.Value)
            {
                return Forbid();
            }

            return Ok(response);
        }

        [HttpGet("byuid/{id}/bygameid/{gameid}")]
        public async Task<ActionResult<WishedGame>> GetWishedGameByUser(Guid id, Guid gameid)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null || id != currentUserId.Value)
            {
                return Forbid();
            }

            var response = await _wishedGameRepositories.GetByUserAndGameId(id, gameid);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWishedGame(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _wishedGameRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            await _wishedGameRepositories.DeleteWishedGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWishedGame(Guid id, [FromBody] WishedGameModel game)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _wishedGameRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            // userId is intentionally not taken from the client — ownership of a wish
            // can never be reassigned to another user.
            var result = await _wishedGameRepositories.UpdateWishedGame(new WishedGame(id, game.ownedGameId, existing.userId, game.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<WishedGame>>> GetAllWishedGamesByIds([FromBody] List<Guid> guidList)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var response = await _wishedGameRepositories.GetIds(guidList);

            // Only the caller's own wishlist rows are ever returned, regardless of
            // which ids were requested.
            return Ok(response.Where(w => w != null && w.userId == currentUserId.Value).ToList());
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
