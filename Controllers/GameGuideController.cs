using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Services.Minio;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameGuideController : Controller
    {
        private readonly IGameGuideRepository _gameGuideRepositories;
        private readonly IMinioService _minioService;

        public GameGuideController(IGameGuideRepository gameGuideRepositories, IMinioService minioService)
        {
            _gameGuideRepositories = gameGuideRepositories;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IGameGuideRepository>>> GetAllGameGuides()
        {
            var _gameGuides = await _gameGuideRepositories.GetAllGameGuides();

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

            await _gameGuideRepositories.Add(result );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameGuide>> GetGameGroup(Guid id)
        {
            var response = await _gameGuideRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameGroup(Guid id)
        {
            await _gameGuideRepositories.DeleteGameGuide(id);
            return NoContent();
        }

        [HttpPut("{id}")]
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
            var result = await _gameGuideRepositories.UpdateGameGuide(new GameGuide(id, game.title, game.description, game.likesCount, game.gameId, game.authorId, game.gameGroupId, game.content, game.contentUrl, game.createdAt));
            return Ok(result);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameGuide>>> GetByGameId(Guid id)
        {
            var response = await _gameGuideRepositories.GetByGameId(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameGuide>>> GetAllGameGuidesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameGuideRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
