using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameGuideController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameGuideDao _gameGuideDao;

        public GameGuideController(DataContext dataContext, GameGuideDao gameGuideDao)
        {
            _dataContext = dataContext;
            _gameGuideDao = gameGuideDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameGuideDao>>> GetAllGameGuides()
        {
            var _gameGuides = await _gameGuideDao.GetAllGameGuides();

            var response = _gameGuides.Select(g => new GameGuide(id: g.id,
                                                                 title: g.title,
                                                                 description: g.description,
                                                                 likesCount: g.likesCount,
                                                                 gameId: g.gameId,
                                                                 authorId: g.authorId,
                                                                 gameGroupId: g.gameGroupId,
                                                                 content: g.content,
                                                                 createdAt: g.createdAt)).ToList();

            return Ok(response);
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
            var response = await _dataContext.dbGameGuides.AddAsync(result);

            return Ok(response);
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
            var result = new GameGuide(id, game.title, game.description, game.likesCount,  game.gameId, game.authorId, game.gameGroupId, game.content, game.createdAt);
            await _gameGuideDao.UpdateGameGuide(result);
            return NoContent();
        }
    }
}
