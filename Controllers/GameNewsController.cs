using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameNewsController : Controller
    {
        private readonly GameNewsDao _gameNewsDao;
        private readonly MinioService _minioService;

        public GameNewsController(GameNewsDao gameNewsDao, MinioService minioService)
        {
            _gameNewsDao = gameNewsDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameNewsDao>>> GetAllGameNews()
        {
            var _gameNews = await _gameNewsDao.GetAllGameNews();

            return Ok(_gameNews);
        }


        [HttpPost]
        public async Task<ActionResult<GameNews>> CreateNews([FromBody] GameNewsModel model, IFormFile? file)
        {
            var result = new GameNews(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                model.gameGroupId,
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

            await _gameNewsDao.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GameNews>> GetGameNews(Guid id)
        {
            var response = await _gameNewsDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameNews(Guid id)
        {
            await _gameNewsDao.DeleteGameNews(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateGameNews(Guid id, [FromBody] GameNewsModel game, IFormFile? file)
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

            var result = await _gameNewsDao.UpdateGameNews(new GameNews(id, game.title, game.description, game.likesCount, game.gameId, game.gameGroupId, game.authorId, game.content, game.contentUrl, game.createdAt));
            return Ok(result);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameNews>>> GetByGameId(Guid id)
        {
            var response = await _gameNewsDao.GetByGameId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameNews>>> GetAllGameNewsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameNewsDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
