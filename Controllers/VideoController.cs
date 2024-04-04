using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[videoController]")]
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

            var response = videos.Select(v => new Video(v.id,
                                                                v.title,
                                                                v.description,
                                                                v.likesCount,
                                                                v.dislikesCount,
                                                                v.gameId,
                                                                v.authorId,
                                                                v.videoUrl)).ToList();
            return Ok(response);
        }

    }
}
