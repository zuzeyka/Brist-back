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
                                                              dislikesCount: g.dislikesCount,
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
                0,
                model.gameId,
                model.authorId,
                model.content,
                                            DateTime.Now
                                            );
            var response = _dataContext.dbGameNews.AddAsync(result);

            return Ok(response);
        }
    }
}
