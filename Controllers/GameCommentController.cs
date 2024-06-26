using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameCommentController : Controller
    {
        private readonly IGameCommentRepository _gameCommentRepositories;

        public GameCommentController(IGameCommentRepository GameGroupRepository)
        {
            _gameCommentRepositories = GameGroupRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<IGameCommentRepository>>> GetAllGameComments()
        {
            var _gameComment = await _gameCommentRepositories.GetAllGameComments();

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
            await _gameCommentRepositories.Add(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameComment>> GetGameComment(Guid id)
        {
            var response = await _gameCommentRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameComment(Guid id)
        {
            await _gameCommentRepositories.DeleteGameComment(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameComment(Guid id, [FromBody] GameCommentModel game)
        {
            var result = await _gameCommentRepositories.UpdateGameComment(new GameComment(id, game.gamePostId, game.content, game.authorId, game.createdAt));
            return Ok(result);
        }

        [HttpGet("bygamepost/{id}")]
        public async Task<ActionResult<List<GameComment>>> GetByGamePostId(Guid id)
        {
            var response = await _gameCommentRepositories.GetByGamePostId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameComment>>> GetAllGameCommentsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameCommentRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
