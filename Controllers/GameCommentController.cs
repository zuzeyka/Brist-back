using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameCommentController : Controller
    {
        private readonly GameCommentDao _gameCommentDao;

        public GameCommentController(GameCommentDao gameGroupDao)
        {
            _gameCommentDao = gameGroupDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameCommentDao>>> GetAllGameComments()
        {
            var _gameComment = await _gameCommentDao.GetAllGameComments();

            return Ok(_gameComment);
        }

        [HttpPost]
        public async Task<ActionResult<GameComment>> CreateGameComment([FromBody] GameCommentModel model)
        {
            var result = new GameComment(Guid.NewGuid(),
                                            model.gamePostId, 
                                            model.content,
                                            model.authorId,
                                            DateTime.Now
                                            );
            await _gameCommentDao.Add(result);
            return Ok(result);
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

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateGameComment(Guid id, [FromBody] GameCommentModel game)
        {
            var result = await _gameCommentDao.UpdateGameComment(new GameComment(id, game.gamePostId, game.content, game.authorId, game.createdAt));
            return Ok(result);
        }

        [HttpGet("bygamepost/{id}")]
        public async Task<ActionResult<List<GameComment>>> GetByGamePostId(Guid id)
        {
            var response = await _gameCommentDao.GetByGamePostId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameComment>>> GetAllGameCommentsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameCommentDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
