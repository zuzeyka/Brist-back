using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameGuideController : Controller
    {
        private readonly GameGuideDao _gameGuideDao;

        public GameGuideController(GameGuideDao gameGuideDao)
        {
            _gameGuideDao = gameGuideDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameGuideDao>>> GetAllGameGuides()
        {
            var _gameGuides = await _gameGuideDao.GetAllGameGuides();

            return Ok(_gameGuides);
        }


        [HttpPost]
        public async Task<ActionResult<GameGuide>> CreateGuide([FromBody] GameGuideModel model)
        {
            var result = new GameGuide(Guid.NewGuid(),
                                            model.title,
                                            model.description,
                                            0,
                                            model.gameId,
                                            model.authorId,
                                            model.gameGroupId,
                                            model.content,
                                            DateTime.Now
                                            );
            await _gameGuideDao.Add(result );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameGuide>> GetGameGroup(Guid id)
        {
            var response = await _gameGuideDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameGroup(Guid id)
        {
            await _gameGuideDao.DeleteGameGuide(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameGroup(Guid id, [FromBody] GameGuideModel game)
        {
            var result = await _gameGuideDao.UpdateGameGuide(new GameGuide(id, game.title, game.description, game.likesCount, game.gameId, game.authorId, game.gameGroupId, game.content, game.createdAt));
            return Ok(result);
        }
    }
}
