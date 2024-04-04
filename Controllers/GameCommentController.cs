using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[gameCommentController]")]
    public class GameCommentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameCommentDao _gameCommentDao;

        public GameCommentController(DataContext dataContext, GameCommentDao gameGroupDao)
        {
            _dataContext = dataContext;
            _gameCommentDao = gameGroupDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameCommentDao>>> GetAllGameComments()
        {
            var _gameComment = await _gameCommentDao.GetAllGameComments();

            var response = _gameComment.Select(g => new GameComment(g.id,
                                                                                g.gamePostId,
                                                                                g.content)).ToList();

            return Ok(response);
        }
    }
}
