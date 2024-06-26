using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Data.Entity.Profile;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishedGameController : Controller
    {
        private readonly WishedGameRepository _wishedGameRepositories;

        public WishedGameController(WishedGameRepository wishedGameRepositories)
        {
            _wishedGameRepositories = wishedGameRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<WishedGameRepository>>> GetAllWishedGames()
        {
            var wishedGames = await _wishedGameRepositories.GetAllWishedGames();

            return Ok(wishedGames);
        }


        [HttpPost]
        public async Task<ActionResult<WishedGame>> CreateWishedGame([FromBody] WishedGameModel model)
        {
            var result = new WishedGame(Guid.NewGuid(),
                model.ownedGameId,
                model.userId,
                DateTime.Now);

            await _wishedGameRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetWishedGame(Guid id)
        {
            var response = await _wishedGameRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("byuid/{id}/bygameid/{gameid}")]
        public async Task<ActionResult<User>> GetWishedGameByUser(Guid id, Guid gameid)
        {
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
            await _wishedGameRepositories.DeleteWishedGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWishedGame(Guid id, [FromBody] WishedGameModel game)
        {
            var result = await _wishedGameRepositories.UpdateWishedGame(new WishedGame(id, game.ownedGameId, game.userId, game.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<WishedGame>>> GetAllWishedGamesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _wishedGameRepositories.GetIds(guidList);

            return Ok(response);
        }
    }
}
