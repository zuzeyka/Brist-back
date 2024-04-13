using FullStackBrist.Server.Models.Creators;
using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreenshotController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ScreenshotDao _screenshotDao;

        public ScreenshotController(DataContext dataContext, ScreenshotDao screenshotDao)
        {
            _dataContext = dataContext;
            _screenshotDao = screenshotDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScreenshotDao>>> GetAllScreenshots()
        {
            var _screenshots = await _screenshotDao.GetAllScreenshots();

            var response = _screenshots.Select(s => new Screenshot(s.id,
                                                                                s.title,
                                                                                s.description,
                                                                                s.likesCount,
                                                                                s.dislikesCount,
                                                                                s.discussionId,
                                                                                s.gameId,
                                                                                s.authorId,
                                                                                s.screenshotUrl,
                                                                                s.createdAt)).ToList();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<Screenshot>> CreateScreenshot([FromBody] ScreenshotModel model)
        {
            var result = new Screenshot(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                0,
                model.discussionId,
                model.gameId,
                model.authorId,
                model.screenshotUrl,
                DateTime.Now);

            _dataContext.dbScreenshots.AddAsync(result);

            return result;
        }
    }
}
