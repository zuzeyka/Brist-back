using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly VideoDao _videoDao;

        public VideoController(DataContext dataContext, VideoDao videoDao)
        {
            _dataContext = dataContext;
            _videoDao = videoDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoDao>>> GetAllVideos()
        {
            var videos = await _videoDao.GetAllVideos();

            var response = videos.Select(v => new Video(id: v.id,
                                                        title: v.title,
                                                        description: v.description,
                                                        likesCount: v.likesCount,
                                                        gameId: v.gameId,
                                                        authorId: v.authorId,
                                                        videoUrl: v.videoUrl,
                                                        createdAt: v.createdAt)).ToList();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<Video>> CreateVideo([FromBody] VideoModel model)
        {
            var result = new Video(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                model.authorId,
                model.videoUrl,
                DateTime.Now);

            var response = await _dataContext.dbVideos.AddAsync(result);

            return Ok(response);
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
        public async Task<ActionResult> UpdateVideo(Guid id, [FromBody] VideoModel video)
        {
            var result = new Video(id, video.title, video.description, video.likesCount, video.gameId, video.authorId, video.videoUrl, video.createdAt);
            await _videoDao.UpdateVideo(result);
            return NoContent();
        }

    }
}
