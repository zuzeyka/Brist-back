using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameNewsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameNewsDao _gameNewsDao;

        public GameNewsController(DataContext dataContext, GameNewsDao gameNewsDao)
        {
            _dataContext = dataContext;
            _gameNewsDao = gameNewsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameNewsDao>>> GetAllGameNews()
        {
            var _gameNews = await _gameNewsDao.GetAllGameNews();

            var response = _gameNews.Select(g => new GameNews(id: g.id,
                                                              title: g.title,
                                                              description: g.description,
                                                              likesCount: g.likesCount,
                                                              gameGroupId: g.gameGroupId,
                                                              gameId: g.gameId,
                                                              authorId: g.authorId,
                                                              content: g.content,
                                                              createdAt: g.createdAt)).ToList();
            return Ok(response);
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
            var response = await _dataContext.dbGameNews.AddAsync(result);

            return Ok(response);
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
            var result = new GameNews(id, game.title, game.description, game.likesCount,  game.gameId, game.gameGroupId, game.authorId, game.content, game.createdAt);
            await _gameNewsDao.UpdateGameNews(result);
            return NoContent();
        }
    }
}
