using FullStackBrist.Server.Models.Creators;
using FullStackBrist.Server.Models.GameGroup;
using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CreatorsDao;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Entity.Profile;
using Slush.Entity.Store.Product.Creators;

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

            var response = _gameComment.Select(g => new GameComment(id: g.id,
                                                                    gamePostId: g.gamePostId,
                                                                    content: g.content,
                                                                    createdAt: g.createdAt)).ToList();

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
            var response = _dataContext.dbGameComments.AddAsync(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameComment>> GetGameComment(Guid id)
        {
            var response = await _gameCommentDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameComment(Guid id)
        {
            await _gameCommentDao.DeleteGameComment(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameComment(Guid id, [FromBody] GameCommentModel game)
        {
            var gameRes = new GameComment(id, game.gamePostId, game.content, game.createdAt);
            await _gameCommentDao.UpdateGameComment(gameRes);
            return NoContent();
        }
    }
}
