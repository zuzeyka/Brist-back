using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[gameNewsController]")]
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

            var response = _gameNews.Select(g => new GameNews(g.id,
                                                                        g.title,
                                                                        g.description,
                                                                        g.likesCount,
                                                                        g.dislikesCount,
                                                                        g.gameId,
                                                                        g.authorId,
                                                                        g.content)).ToList();
            return Ok(response);
        }
    }
}
