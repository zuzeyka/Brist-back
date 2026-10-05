using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Store.Product;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameEventController : Controller
    {
        private readonly IGameEventRepository _gameEventRepositories;

        public GameEventController(IGameEventRepository gameEventRepositories)
        {
            _gameEventRepositories = gameEventRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameEvent>>> GetAllGameEvents()
        {
            var events = await _gameEventRepositories.GetAll();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameEvent>> GetGameEvent(Guid id)
        {
            var response = await _gameEventRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GameEvent>> CreateGameEvent([FromBody] GameEventModel model)
        {
            var result = new GameEvent(Guid.NewGuid(), model.name, model.description, model.startAt, model.endAt, DateTime.Now);
            await _gameEventRepositories.Add(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameEvent(Guid id, [FromBody] GameEventModel model)
        {
            var result = await _gameEventRepositories.UpdateGameEvent(new GameEvent(id, model.name, model.description, model.startAt, model.endAt, model.createdAt));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameEvent(Guid id)
        {
            await _gameEventRepositories.DeleteGameEvent(id);
            return NoContent();
        }
    }
}
