using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameGroupController : Controller
    {
        private readonly GameGroupDao _gameGroupDao;

        public GameGroupController(GameGroupDao gameGroupDao)
        {
            _gameGroupDao = gameGroupDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameGroupDao>>> GetAllGameGroups()
        {
            var gameGroups = await _gameGroupDao.GetAllGameGroups();

            return Ok(gameGroups);
        }

        [HttpPost]
        public async Task<ActionResult<GameGroup>> CreateGroup([FromBody] GameGroupModel model)
        {
            var result = new GameGroup(Guid.NewGuid(),
                                            model.gameId,
                                            DateTime.Now
                                            );
            await _gameGroupDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameGroup>> GetGameGroup(Guid id)
        {
            var response = await _gameGroupDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameGroup(Guid id)
        {
            await _gameGroupDao.DeleteGameGroup(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameGroup(Guid id, [FromBody] GameGroupModel game)
        {
            var result = await _gameGroupDao.UpdateGameGroup(new GameGroup(id, game.gameId, game.createdAt));
            return Ok(result);
        }
    }
}
