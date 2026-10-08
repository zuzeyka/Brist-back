using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity.Profile;
using Slush.Services.FileStorage;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreenshotController : Controller
    {
        // Screenshots are public game-page content (like reviews/comments) — GET
        // endpoints stay anonymous. Only the author can create/edit/delete their own.
        private readonly IScreenshotRepository _screenshotRepositories;
        private readonly IFileStorageService _fileStorageService;

        public ScreenshotController(IScreenshotRepository screenshotRepositories, IFileStorageService fileStorageService)
        {
            _screenshotRepositories = screenshotRepositories;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IScreenshotRepository>>> GetAllScreenshots()
        {
            var _screenshots = await _screenshotRepositories.GetAllScreenshots();

            return Ok(_screenshots);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Screenshot>> CreateScreenshot([FromBody] ScreenshotModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // A screenshot can only ever be posted under the caller's own authorship;
            // client-supplied model.authorId is ignored to prevent impersonating another user.
            var result = new Screenshot(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                currentUserId.Value,
                model.contentUrl,
                DateTime.Now);

            await _screenshotRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Screenshot>> GetScreenshot(Guid id)
        {
            var response = await _screenshotRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<Screenshot>>> GetScreenshotByGame(Guid id)
        {
            var response = await _screenshotRepositories.GetByGameId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteScreenshot(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _screenshotRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.authorId != currentUserId.Value)
            {
                return Forbid();
            }

            await _screenshotRepositories.DeleteScreenshot(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateScreenshot(Guid id, [FromBody] ScreenshotModel screenshot, IFormFile? file)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _screenshotRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.authorId != currentUserId.Value)
            {
                return Forbid();
            }

            if (file != null && file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _fileStorageService.SaveFile("images", id, file.FileName, stream);

                        var url = await _fileStorageService.GetUrlToFile(imageUrl);

                        screenshot.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            // authorId is intentionally not taken from the client — authorship of a
            // screenshot can never be reassigned to another user.
            var result = await _screenshotRepositories.UpdateScreenshot(new Screenshot(id, screenshot.title, screenshot.description, screenshot.likesCount, screenshot.gameId, existing.authorId, screenshot.contentUrl, screenshot.createdAt));
            return Ok(result);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<Screenshot>>> GetByUserId(Guid id)
        {
            var response = await _screenshotRepositories.GetByUserId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Screenshot>>> GetAllScreenshotsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _screenshotRepositories.GetByIds(guidList);

            return Ok(response);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
