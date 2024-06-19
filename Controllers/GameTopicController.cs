using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameTopicController : Controller
    {
        private readonly GameTopicDao _gameTopicDao;

        public GameTopicController(GameTopicDao gameTopicDao)
        {
            _gameTopicDao = gameTopicDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameTopicDao>>> GetAllGameTopics()
        {
            var gameTopics = await _gameTopicDao.GetAllGameTopics();

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
            await _gameTopicDao.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GameTopic>> GetGameTopic(Guid id)
        {
            var response = await _gameTopicDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameTopic(Guid id)
        {
            await _gameTopicDao.DeleteGameTopic(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameNews(Guid id, [FromBody] GameTopicModel game)
        {
            var result = await _gameTopicDao.UpdateGameTopic(new GameTopic(id, game.attachedId, game.name, game.description, game.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameTopic>>> GetAllGameNewsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameTopicDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
