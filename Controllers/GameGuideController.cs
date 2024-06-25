using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupRepository;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity.Profile;
using Slush.Services.Minio;
using System.Runtime.CompilerServices;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameGuideController : Controller
    {
        private readonly GameGuideRepository _gameGuideDao;
        private readonly MinioService _minioService;

        public GameGuideController(GameGuideRepository gameGuideDao, MinioService minioService)
        {
            _gameGuideDao = gameGuideDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameGuideRepository>>> GetAllGameGuides()
        {
            var _gameGuides = await _gameGuideDao.GetAllGameGuides();

            return Ok(_gameGuides);
        }


        [HttpPost]
        public async Task<ActionResult<GameGuide>> CreateGuide([FromBody] GameGuideModel model, IFormFile? file)
        {
            var result = new GameGuide(Guid.NewGuid(),
                                            model.title,
                                            model.description,
                                            0,
                                            model.gameId,
                                            model.authorId,
                                            model.gameGroupId,
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

            await _gameGuideDao.Add(result );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameGuide>> GetGameGroup(Guid id)
        {
            var response = await _gameGuideDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameGroup(Guid id)
        {
            await _gameGuideDao.DeleteGameGuide(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateGameGroup(Guid id, [FromBody] GameGuideModel game, IFormFile? file)
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
            var result = await _gameGuideDao.UpdateGameGuide(new GameGuide(id, game.title, game.description, game.likesCount, game.gameId, game.authorId, game.gameGroupId, game.content, game.contentUrl, game.createdAt));
            return Ok(result);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameGuide>>> GetByGameId(Guid id)
        {
            var response = await _gameGuideDao.GetByGameId(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameGuide>>> GetAllGameGuidesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameGuideDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
