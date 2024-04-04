using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[gamePostController]")]
    public class GamePostController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GamePostsDao _gamePostsDao;

        public GamePostController(DataContext dataContext, GamePostsDao gamePostsDao)
        {
            _dataContext = dataContext;
            _gamePostsDao = gamePostsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GamePostsDao>>> GetAllGamePosts()
        {
            var gamePosts = await _gamePostsDao.GetAllGamePosts();

            var response = gamePosts.Select(g => new GamePosts(g.id,
                                                                            g.title,
                                                                            g.description,
                                                                            g.likesCount,
                                                                            g.dislikesCount,
                                                                            g.discussionId,
                                                                            g.gameId,
                                                                            g.authorId,
                                                                            g.content)).ToList();
            return Ok(response);
        }
    }
}
