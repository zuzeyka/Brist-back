using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreenshotController : Controller
    {
        private readonly ScreenshotDao _screenshotDao;

        public ScreenshotController(ScreenshotDao screenshotDao)
        {
            _screenshotDao = screenshotDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScreenshotDao>>> GetAllScreenshots()
        {
            var _screenshots = await _screenshotDao.GetAllScreenshots();

            return Ok(_screenshots);
        }


        [HttpPost]
        public async Task<ActionResult<Screenshot>> CreateScreenshot([FromBody] ScreenshotModel model)
        {
            var result = new Screenshot(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                model.authorId,
                model.screenshotUrl,
                DateTime.Now);

            await _screenshotDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Screenshot>> GetScreenshot(Guid id)
        {
            var response = await _screenshotDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScreenshot(Guid id)
        {
            await _screenshotDao.DeleteScreenshot(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScreenshot(Guid id, [FromBody] ScreenshotModel screenshot)
        {
            await _screenshotDao.UpdateScreenshot(new Screenshot(id, screenshot.title, screenshot.description, screenshot.likesCount, screenshot.gameId, screenshot.authorId, screenshot.screenshotUrl, screenshot.createdAt));
            return NoContent();
        }
    }
}
