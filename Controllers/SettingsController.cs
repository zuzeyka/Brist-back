using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Profile;
using Slush.Models.Profile;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsRepository _settingsRepositories;

        public SettingsController(ISettingsRepository settingsRepositories)
        {
            _settingsRepositories = settingsRepositories;
        }

        // Dropped: an unauthenticated "GetAll" used to return every user's notification
        // preferences at once. Nothing in the frontend calls it, and GetByUserId already
        // covers the only legitimate self-lookup use case.

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Settings>> GetById(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var response = await _settingsRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            if (currentUserId == null || response.attachedUserId != currentUserId.Value)
            {
                return Forbid();
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getbyuid/{id}")]
        public async Task<ActionResult<Settings>> GetByUserId(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null || id != currentUserId.Value)
            {
                return Forbid();
            }

            var response = await _settingsRepositories.GetByUserId(id);

            if (response == null)
            {
                return NotFound(id);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Settings>> CreateSettings([FromBody] SettingsModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // Settings can only ever be created for the caller themselves; client-supplied
            // model.attachedUserId is ignored to prevent creating/overwriting another user's prefs.
            var settings = new Settings(Guid.NewGuid(),
                currentUserId.Value,
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
            var currentUserId = GetCurrentUserId();
            var existing = await _settingsRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.attachedUserId != currentUserId.Value)
            {
                return Forbid();
            }

            await _settingsRepositories.DeleteSettings(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] SettingsModel model)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _settingsRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.attachedUserId != currentUserId.Value)
            {
                return Forbid();
            }

            // attachedUserId is intentionally not taken from the client — ownership of a
            // settings row can never be reassigned to another user.
            var result = await _settingsRepositories.UpdateSettings(new Settings(id,
                existing.attachedUserId,
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
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var response = await _settingsRepositories.GetByIds(guidList);

            // Only the caller's own settings row is ever returned, regardless of which
            // ids were requested.
            return Ok(response.Where(s => s != null && s.attachedUserId == currentUserId.Value).ToList());
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
