using FullStackBrist.Server.Models.GameGroup;
using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                                                                                g.content,
                                                                                g.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GameComment>> CreateGameComment([FromBody] GameCommentModel model)
        {
            var result = new GameComment(Guid.NewGuid(),
                                            model.gamePostId, 
                                            model.content,
                                            DateTime.Now
                                            );
            _dataContext.dbGameComments.AddAsync(result);

            return result;
        }
    }
}
