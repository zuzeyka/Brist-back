using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamePostController : Controller
    {
        private readonly GamePostsDao _gamePostsDao;

        public GamePostController(GamePostsDao gamePostsDao)
        {
            _gamePostsDao = gamePostsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GamePostsDao>>> GetAllGamePosts()
        {
            var gamePosts = await _gamePostsDao.GetAllGamePosts();

            return Ok(gamePosts);
        }



        [HttpPost]
        public async Task<ActionResult<GamePosts>> CreateGamePost([FromBody] GamePostsModel model)
        {
            var result = new GamePosts(Guid.NewGuid(),
                                            model.title,
                                            model.description,
                                            0,
                                            model.gameId,
                                            model.gameTopicId,
                                            model.authorId,
                                            model.content,
                                            DateTime.Now
                                            );

            await _gamePostsDao.Add(result);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GamePosts>> GetGamePosts(Guid id)
        {
            var response = await _gamePostsDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGamePosts(Guid id)
        {
            await _gamePostsDao.DeleteGamePosts(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGamePosts(Guid id, [FromBody] GamePostsModel game)
        {
            await _gamePostsDao.UpdateGamePosts(new GamePosts(id, game.title, game.description, game.likesCount, game.gameId, game.gameTopicId, game.authorId, game.content, game.createdAt));
            return NoContent();
        }
    }
}
