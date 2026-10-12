using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetMine()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var response = await _notificationRepository.GetByUserId(currentUserId.Value);
            return Ok(response);
        }

        [HttpPut("markread/{id}")]
        public async Task<ActionResult> MarkRead(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _notificationRepository.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            var result = await _notificationRepository.MarkRead(id);
            return Ok(result);
        }

        [HttpPut("markallread")]
        public async Task<ActionResult> MarkAllRead()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            await _notificationRepository.MarkAllRead(currentUserId.Value);
            return NoContent();
        }

        [HttpDelete("clearall")]
        public async Task<ActionResult> ClearAll()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            await _notificationRepository.ClearAll(currentUserId.Value);
            return NoContent();
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
