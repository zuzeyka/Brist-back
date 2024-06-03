using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Entity.Profile;
using Slush.Models.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly SettingsDao _settingsDao;

        public SettingsController(DataContext dataContext, SettingsDao settingsDao)
        {
            _dataContext = dataContext;
            _settingsDao = settingsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<SettingsDao>>> GetAll()
        {
            var settings = await _settingsDao.GetAll();
            var response = settings.Select(s => new Settings(
                id: s.id,
                attachedUserId: s.attachedUserId,
                bigSaleNotification: s.bigSaleNotification,
                saleFromWishlistNotification: s.saleFromWishlistNotification,
                newCommentNotification: s.newCommentNotification,
                friendRequestNotification: s.friendRequestNotification,
                approvedFriendRequest: s.approvedFriendRequest,
                declinedFriendRequest: s.declinedFriendRequest,
                createdAt: s.createdAt
                )).ToList();

            return Ok(response);
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
            var settings = new Settings(id,
                model.attachedUserId,
                model.bigSaleNotification,
               model.saleFromWishlistNotification,
               model.newCommentNotification,
               model.friendRequestNotification,
               model.approvedFriendRequestNotification,
               model.declinedFriendRequestNotification,
               DateTime.Now);

            await _settingsDao.UpdateSettings(settings);

            return NoContent();
        }
    }
}
