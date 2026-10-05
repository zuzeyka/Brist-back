using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Store.Product;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameEventForGameController : Controller
    {
        private readonly IGameEventForGameRepository _gameEventForGameRepositories;

        public GameEventForGameController(IGameEventForGameRepository gameEventForGameRepositories)
        {
            _gameEventForGameRepositories = gameEventForGameRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameEventForGame>>> GetAll()
        {
            var links = await _gameEventForGameRepositories.GetAll();

            return Ok(links);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameEventForGame>> GetById(Guid id)
        {
            var response = await _gameEventForGameRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameEventForGame>>> GetByGameId(Guid id)
        {
            var response = await _gameEventForGameRepositories.GetByGameId(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GameEventForGame>> Create([FromBody] GameEventForGameModel model)
        {
            var result = new GameEventForGame(Guid.NewGuid(), model.gameId, model.eventId, DateTime.Now);
            await _gameEventForGameRepositories.Add(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _gameEventForGameRepositories.DeleteGameEventForGame(id);
            return NoContent();
        }
    }
}
