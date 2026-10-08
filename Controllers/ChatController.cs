using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ChatRepository;
using Slush.Entity.Chat;
using Slush.Models.Chat;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatRepository _ChatRepository;

        public ChatController(IChatRepository ChatRepository)
        {
            _ChatRepository = ChatRepository;
        }

        // Dropped: an unauthenticated "GetAllChats" used to return every chat between
        // every pair of users in one call. Nothing in the frontend calls it.

        [HttpPost]
        public async Task<ActionResult<Chat>> CreateChat([FromBody] ChatModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null || (currentUserId.Value != model.firstUser && currentUserId.Value != model.secondUser))
            {
                return Forbid();
            }

            // Only one chat may ever exist between a given pair of users — return the
            // existing one instead of creating a duplicate on a repeated/racing call.
            var existing = await _ChatRepository.GetBetweenUsers(model.firstUser, model.secondUser);
            if (existing != null)
            {
                return Ok(existing);
            }

            var result = new Chat(Guid.NewGuid(), model.firstUser, model.secondUser, DateTime.Now);
            await _ChatRepository.AddChat(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var response = await _ChatRepository.GetById(id);

            if(response == null)
            {
                return NotFound();
            }
            if (currentUserId == null || (response.firstUser != currentUserId.Value && response.secondUser != currentUserId.Value))
            {
                return Forbid();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _ChatRepository.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || (existing.firstUser != currentUserId.Value && existing.secondUser != currentUserId.Value))
            {
                return Forbid();
            }

            await _ChatRepository.DeleteChat(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChat(Guid id, [FromBody] ChatModel model)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _ChatRepository.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || (existing.firstUser != currentUserId.Value && existing.secondUser != currentUserId.Value))
            {
                return Forbid();
            }

            // firstUser/secondUser are intentionally not taken from the client — the
            // two participants of a chat can never be reassigned through this endpoint.
            var result = await _ChatRepository.UpdateChat(new Chat(id, existing.firstUser, existing.secondUser, DateTime.Now));

            return Ok(result);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<Chat>>> GetChatsByUserId(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null || id != currentUserId.Value)
            {
                return Forbid();
            }

            var response = await _ChatRepository.GetByUserId(id);

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Chat>>> GetAllChatsByIds([FromBody] List<Guid> guidList)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var response = await _ChatRepository.GetByIds(guidList);

            // Only chats the caller participates in are ever returned, regardless of
            // which ids were requested.
            return Ok(response.Where(c => c != null && (c.firstUser == currentUserId.Value || c.secondUser == currentUserId.Value)).ToList());
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
