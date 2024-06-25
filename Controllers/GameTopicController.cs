using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.GameGroupRepository;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameTopicController : Controller
    {
        private readonly GameTopicRepository _gameTopicRepositories;

        public GameTopicController(GameTopicRepository gameTopicRepositories)
        {
            _gameTopicRepositories = gameTopicRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameTopicRepository>>> GetAllGameTopics()
        {
            var gameTopics = await _gameTopicRepositories.GetAllGameTopics();

            return Ok(gameTopics);
        }


        [HttpPost]
        public async Task<ActionResult<GameTopic>> CreateGameTopic([FromBody] GameTopicModel model)
        {
            var result = new GameTopic(Guid.NewGuid(),
                                            model.attachedId,
                                            model.name,
                                            model.description,
                                            DateTime.Now
                                            );
            await _gameTopicRepositories.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GameTopic>> GetGameTopic(Guid id)
        {
            var response = await _gameTopicRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameTopic(Guid id)
        {
            await _gameTopicRepositories.DeleteGameTopic(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateGameNews(Guid id, [FromBody] GameTopicModel game)
        {
            var result = await _gameTopicRepositories.UpdateGameTopic(new GameTopic(id, game.attachedId, game.name, game.description, game.createdAt));
            return Ok(result);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameTopic>>> GetByGameId(Guid id)
        {
            var response = await _gameTopicRepositories.GetByGameId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameTopic>>> GetAllGameNewsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameTopicRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
