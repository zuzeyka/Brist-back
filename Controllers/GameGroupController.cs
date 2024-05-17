using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameGroupController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameGroupDao _gameGroupDao;

        public GameGroupController(DataContext dataContext, GameGroupDao gameGroupDao)
        {
            _dataContext = dataContext;
            _gameGroupDao = gameGroupDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameGroupDao>>> GetAllGameGroups()
        {
            var gameGroups = await _gameGroupDao.GetAllGameGroups();

            var response = gameGroups.Select(g => new GameGroup(id: g.id,
                                                                gameId: g.gameId,
                                                                createdAt: g.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GameGroup>> CreateGroup([FromBody] GameGroupModel model)
        {
            var result = new GameGroup(Guid.NewGuid(),
                                            model.gameId,
                                            DateTime.Now
                                            );
            var response = await _dataContext.dbGameGroups.AddAsync(result);

            return Ok(response);
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
            var result = new GameGroup(id, game.gameId, game.createdAt);
            await _gameGroupDao.UpdateGameGroup(result);
            return NoContent();
        }
    }
}
