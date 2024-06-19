using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameNewsController : Controller
    {
        private readonly GameNewsDao _gameNewsDao;

        public GameNewsController(GameNewsDao gameNewsDao)
        {
            _gameNewsDao = gameNewsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameNewsDao>>> GetAllGameNews()
        {
            var _gameNews = await _gameNewsDao.GetAllGameNews();

            return Ok(_gameNews);
        }


        [HttpPost]
        public async Task<ActionResult<GameNews>> CreateNews([FromBody] GameNewsModel model)
        {
            var result = new GameNews(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                model.gameGroupId,
                model.authorId,
                model.content,
                                            DateTime.Now
                                            );
            await _gameNewsDao.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GameNews>> GetGameNews(Guid id)
        {
            var response = await _gameNewsDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameNews(Guid id)
        {
            await _gameNewsDao.DeleteGameNews(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameNews(Guid id, [FromBody] GameNewsModel game)
        {
            var result = await _gameNewsDao.UpdateGameNews(new GameNews(id, game.title, game.description, game.likesCount, game.gameId, game.gameGroupId, game.authorId, game.content, game.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameNews>>> GetAllGameNewsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameNewsDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
