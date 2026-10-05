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
    public class OwnedGameController : Controller
    {
        private readonly IOwnedGameRepository _ownedGameRepositories;

        public OwnedGameController(IOwnedGameRepository ownedGameRepositories)
        {
            _ownedGameRepositories = ownedGameRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<IOwnedGameRepository>>> GetAllOwnedGames()
        {
            var _ownedGames = await _ownedGameRepositories.GetAllOwnedGames();

            return Ok(_ownedGames);
        }


        [HttpPost]
        public async Task<ActionResult<OwnedGame>> CreateOwnedGame([FromBody] OwnedGameModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // Ownership can only ever be granted to the authenticated caller here;
            // client-supplied model.userId is ignored to prevent granting games to others.
            var result = new OwnedGame(Guid.NewGuid(),
                model.ownedGameId,
                currentUserId.Value,
                                            DateTime.Now
                                            );
            await _ownedGameRepositories.Add(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnedGame>> GetOwnedGame(Guid id)
        {
            var response = await _ownedGameRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<OwnedGame>>> GetByUserId(Guid id)
        {
            var response = await _ownedGameRepositories.GetByUId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}/byuserid/{uid}")]
        public async Task<ActionResult<List<OwnedGame>>> GetUsersByOwnedGame(Guid id, Guid uid)
        {
            var response = await _ownedGameRepositories.GetByGameId(id, uid);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOwnedGame(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _ownedGameRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            await _ownedGameRepositories.DeleteOwnedGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOwnedGame(Guid id, [FromBody] OwnedGameModel game)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _ownedGameRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            // userId is intentionally not taken from the client — ownership of a
            // grant can never be reassigned to another user through this endpoint.
            var result = await _ownedGameRepositories.UpdateOwnedGame(new OwnedGame(id, game.ownedGameId, existing.userId, game.createdAt));
            return Ok(result);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<OwnedGame>>> GetAllOwnedGamesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _ownedGameRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
