using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamePostController : Controller
    {
        private readonly GamePostsDao _gamePostsDao;
        private readonly MinioService _minioService;

        public GamePostController(GamePostsDao gamePostsDao, MinioService minioService)
        {
            _gamePostsDao = gamePostsDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GamePostsDao>>> GetAllGamePosts()
        {
            var gamePosts = await _gamePostsDao.GetAllGamePosts();

            return Ok(gamePosts);
        }



        [HttpPost]
        public async Task<ActionResult<GamePosts>> CreateGamePost([FromBody] GamePostsModel model, IFormFile? file)
        {
            var result = new GamePosts(Guid.NewGuid(),
                                            model.title,
                                            model.description,
                                            0,
                                            model.gameId,
                                            model.authorId,
                                            model.content,
                                            model.contentUrl,
                                            DateTime.Now
                                            );

            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", result.id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        result.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

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



        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GamePosts>>> GetByGameId(Guid id)
        {
            var response = await _gamePostsDao.GetByGameId(id);

            if(response == null)
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
        public async Task<ActionResult> UpdateGamePosts(Guid id, [FromBody] GamePostsModel game, IFormFile? file)
        {
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        game.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            var result = await _gamePostsDao.UpdateGamePosts(new GamePosts(id, game.title, game.description, game.likesCount, game.gameId, game.authorId, game.content, game.contentUrl, game.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GamePosts>>> GetAllGamePostsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gamePostsDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
