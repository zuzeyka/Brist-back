using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Entity.Profile;
using Slush.Models.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly SettingsDao _settingsDao;

        public SettingsController(SettingsDao settingsDao)
        {
            _settingsDao = settingsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<SettingsDao>>> GetAll()
        {
            var settings = await _settingsDao.GetAll();

            return Ok(settings);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Settings>> GetById(Guid id)
        {
            var response = await _settingsDao.GetById(id);
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
            var response = await _settingsDao.GetByUserId(userId);

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
            await _settingsDao.Add(settings);

            return Ok(settings);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSettings(Guid id)
        {
            await _settingsDao.DeleteSettings(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] SettingsModel model)
        {

            await _settingsDao.UpdateSettings(new Settings(id,
                model.attachedUserId,
                model.bigSaleNotification,
               model.saleFromWishlistNotification,
               model.newCommentNotification,
               model.friendRequestNotification,
               model.approvedFriendRequestNotification,
               model.declinedFriendRequestNotification,
               DateTime.Now));

            return NoContent();
        }
    }
}
