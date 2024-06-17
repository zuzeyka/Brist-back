using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data.Entity.Profile;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly VideoDao _videoDao;
        private readonly MinioService _minioService;

        public VideoController(VideoDao videoDao, MinioService minioService)
        {
            _videoDao = videoDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoDao>>> GetAllVideos()
        {
            var videos = await _videoDao.GetAllVideos();

            return Ok(videos);
        }


        [HttpPost]
        public async Task<ActionResult<Video>> CreateVideo([FromBody] VideoModel model, IFormFile file)
        {
            var result = new Video(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                model.authorId,
                model.videoUrl,
                DateTime.Now);

            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", result.id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        result.videoUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            await _videoDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideo(Guid id)
        {
            var response = await _videoDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVideo(Guid id)
        {
            await _videoDao.DeleteVideo(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVideo(Guid id, [FromBody] VideoModel video, IFormFile file)
        {
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        video.videoUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }
            var result = await _videoDao.UpdateVideo(new Video(id, video.title, video.description, video.likesCount, video.gameId, video.authorId, video.videoUrl, video.createdAt));
            return Ok(result);
        }

    }
}
