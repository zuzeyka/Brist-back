using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            var response = gamePosts.Select(g => new GamePosts(id: g.id,
                                                               title: g.title,
                                                               description: g.description,
                                                               likesCount: g.likesCount,
                                                               dislikesCount: g.dislikesCount,
                                                               discussionId: g.discussionId,
                                                               gameTopicId: g.gameTopicId,
                                                               gameId: g.gameId,
                                                               authorId: g.authorId,
                                                               content: g.content,
                                                               createdAt: g.createdAt)).ToList();
            return Ok(response);
        }



        [HttpPost]
        public async Task<ActionResult<GamePosts>> CreateGamePost([FromBody] GamePostsModel model)
        {
            var result = new GamePosts(Guid.NewGuid(),
                                            model.title,
                                            model.description,
                                            0,
                                            0,
                                            model.discussionId,
                                            model.gameId,
                                            model.gameTopicId,
                                            model.authorId,
                                            model.content,
                                            DateTime.Now
                                            );
            var response = _dataContext.dbGamePosts.AddAsync(result);

            return Ok(response);
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
            var result = new GamePosts(id, game.title, game.description, game.likesCount, game.dislikesCount, game.discussionId, game.gameId, game.gameTopicId, game.authorId, game.content, game.createdAt);
            await _gamePostsDao.UpdateGamePosts(result);
            return NoContent();
        }
    }
}
