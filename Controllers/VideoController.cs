using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Data.Entity.Profile;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly VideoRepository _videoRepositories;
        private readonly MinioService _minioService;

        public VideoController(VideoRepository videoRepositories, MinioService minioService)
        {
            _videoRepositories = videoRepositories;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoRepository>>> GetAllVideos()
        {
            var videos = await _videoRepositories.GetAllVideos();

            return Ok(videos);
        }


        [HttpPost]
        public async Task<ActionResult<Video>> CreateVideo([FromBody] VideoModel model, IFormFile? file)
        {
            var result = new Video(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                model.authorId,
                model.contentUrl,
                DateTime.Now);

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
        public async Task<ActionResult> DeleteVideo(Guid id)
        {
            await _videoRepositories.DeleteVideo(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateVideo(Guid id, [FromBody] VideoModel video, IFormFile? file)
        {
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        video.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }
            var result = await _videoRepositories.UpdateVideo(new Video(id, video.title, video.description, video.likesCount, video.gameId, video.authorId, video.contentUrl, video.createdAt));
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
    }
}
