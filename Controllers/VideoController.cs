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
    public class VideoController : Controller
    {
        // Videos are public game-page content (like screenshots/reviews) — GET
        // endpoints stay anonymous. Only the author can create/edit/delete their own.
        private readonly IVideoRepository _videoRepositories;
        private readonly IFileStorageService _fileStorageService;

        public VideoController(IVideoRepository videoRepositories, IFileStorageService fileStorageService)
        {
            _videoRepositories = videoRepositories;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Video>>> GetAllVideos()
        {
            var videos = await _videoRepositories.GetAllVideos();

            return Ok(videos);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Video>> CreateVideo([FromBody] VideoModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // A video can only ever be posted under the caller's own authorship;
            // client-supplied model.authorId is ignored to prevent impersonating another user.
            var result = new Video(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                currentUserId.Value,
                model.contentUrl,
                DateTime.Now);

            await _videoRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideo(Guid id)
        {
            var response = await _videoRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteVideo(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _videoRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.authorId != currentUserId.Value)
            {
                return Forbid();
            }

            await _videoRepositories.DeleteVideo(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateVideo(Guid id, [FromForm] VideoModel video, IFormFile? file)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _videoRepositories.GetById(id);
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
                        String imageUrl = await _fileStorageService.SaveFile("videos", id, file.FileName, stream);

                        var url = await _fileStorageService.GetUrlToFile(imageUrl);

                        video.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            // authorId is intentionally not taken from the client — authorship of a
            // video can never be reassigned to another user.
            var result = await _videoRepositories.UpdateVideo(new Video(id, video.title, video.description, video.likesCount, video.gameId, existing.authorId, video.contentUrl, video.createdAt));
            return Ok(result);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<Video>>> GetByUserId(Guid id)
        {
            var response = await _videoRepositories.GetByUId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<Video>>> GetByGameId(Guid id)
        {
            var response = await _videoRepositories.GetByGameId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Video>>> GetAllVideosByIds([FromBody] List<Guid> guidList)
        {
            var response = await _videoRepositories.GetByIds(guidList);

            return Ok(response);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
