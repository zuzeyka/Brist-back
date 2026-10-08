using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Chat;
using Slush.Models.Chat;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepositories;
        private readonly IChatRepository _chatRepositories;

        public MessageController(IMessageRepository messageRepositories, IChatRepository chatRepositories)
        {
            _messageRepositories = messageRepositories;
            _chatRepositories = chatRepositories;
        }

        // Was [FromBody] Chat chat on a GET route — the route's own {id} was never
        // read, so this always 400'd/ignored the path entirely. Now takes the chat id
        // from the route like every other "by parent id" endpoint in this codebase,
        // and is restricted to the chat's two participants.
        [HttpGet("bychat/{id}")]
        public async Task<ActionResult<List<Message>>> GetAllMessages(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var chat = await _chatRepositories.GetById(id);
            if (chat == null)
            {
                return NotFound();
            }
            if (currentUserId == null || (chat.firstUser != currentUserId.Value && chat.secondUser != currentUserId.Value))
            {
                return Forbid();
            }

            var messages = await _messageRepositories.GetAllMessages(id);

            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage([FromBody] MessageModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var chat = await _chatRepositories.GetById(model.chatId);
            if (chat == null)
            {
                return NotFound();
            }
            if (chat.firstUser != currentUserId.Value && chat.secondUser != currentUserId.Value)
            {
                return Forbid();
            }

            // A message can only ever be sent by the caller themselves; client-supplied
            // model.senderId is ignored to prevent sending as another participant.
            var result = new Message(Guid.NewGuid(), model.chatId, currentUserId.Value, model.content, DateTime.Now);

            await _messageRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetChat(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var response = await _messageRepositories.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            var chat = await _chatRepositories.GetById(response.chatId);
            if (currentUserId == null || chat == null || (chat.firstUser != currentUserId.Value && chat.secondUser != currentUserId.Value))
            {
                return Forbid();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _messageRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            // Only the message's own sender may delete it — unlike reads, the other
            // chat participant has no business deleting someone else's message.
            if (currentUserId == null || existing.senderId != currentUserId.Value)
            {
                return Forbid();
            }

            await _messageRepositories.DeleteMessage(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMessage(Guid id, [FromBody] MessageModel model)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _messageRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.senderId != currentUserId.Value)
            {
                return Forbid();
            }

            // chatId/senderId are intentionally not taken from the client — a message
            // can never be moved to another chat or reassigned to another sender.
            var result = await _messageRepositories.UpdateMessage(new Message(id, existing.chatId, existing.senderId, model.content, DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Message>>> GetAllMessagesByIds([FromBody] List<Guid> guidList)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var response = await _messageRepositories.GetByIds(guidList);

            // Only messages the caller actually sent are ever returned here. (A full
            // "sent to me as well" check would need a chat lookup per message; nothing
            // in the frontend calls this endpoint today, so the stricter sender-only
            // filter is the safe default rather than N extra chat lookups.)
            return Ok(response.Where(m => m != null && m.senderId == currentUserId.Value).ToList());
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
