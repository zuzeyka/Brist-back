using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Entity.Profile;
using Slush.Models.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly SettingsRepository _settingsRepositories;

        public SettingsController(SettingsRepository settingsRepositories)
        {
            _settingsRepositories = settingsRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<SettingsRepository>>> GetAll()
        {
            var settings = await _settingsRepositories.GetAll();

            return Ok(settings);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Settings>> GetById(Guid id)
        {
            var response = await _settingsRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getbyuid/{id}")]
        public async Task<ActionResult<Settings>> GetByUserId(Guid userId)
        {
            var response = await _settingsRepositories.GetByUserId(userId);

            if (response == null)
            {
                return NotFound(userId);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Settings>> CreateSettings([FromBody] SettingsModel model)
        {
            var settings = new Settings(Guid.NewGuid(),
                model.attachedUserId,
                model.bigSaleNotification,
               model.saleFromWishlistNotification,
               model.newCommentNotification,
               model.friendRequestNotification,
               model.approvedFriendRequestNotification,
               model.declinedFriendRequestNotification,
               DateTime.Now);
            await _settingsRepositories.Add(settings);

            return Ok(settings);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSettings(Guid id)
        {
            await _settingsRepositories.DeleteSettings(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] SettingsModel model)
        {

            var result = await _settingsRepositories.UpdateSettings(new Settings(id,
                model.attachedUserId,
                model.bigSaleNotification,
               model.saleFromWishlistNotification,
               model.newCommentNotification,
               model.friendRequestNotification,
               model.approvedFriendRequestNotification,
               model.declinedFriendRequestNotification,
               DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Settings>>> GetAllSettingsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _settingsRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
