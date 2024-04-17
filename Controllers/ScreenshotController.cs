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

            var response = _screenshots.Select(s => new Screenshot(id: s.id,
                                                                   title: s.title,
                                                                   description: s.description,
                                                                   likesCount: s.likesCount,
                                                                   dislikesCount: s.dislikesCount,
                                                                   discussionId: s.discussionId,
                                                                   gameId: s.gameId,
                                                                   authorId: s.authorId,
                                                                   screenshotUrl: s.screenshotUrl,
                                                                   createdAt: s.createdAt)).ToList();
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

            var response = _dataContext.dbScreenshots.AddAsync(result);

            return Ok(response);
        }
    }
}
