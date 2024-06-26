using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ChatRepository;
using Slush.Entity.Chat;
using Slush.Models.Chat;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly MessageRepository _messageRepositories;

        public MessageController(MessageRepository messageRepositories)
        {
            _messageRepositories = messageRepositories;
        }

        [HttpGet("bychat/{id}")]
        public async Task<ActionResult<List<MessageRepository>>> GetAllMessages([FromBody] Chat chat)
        {
            var messages = await _messageRepositories.GetAllMessages(chat.id);

            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage([FromBody] MessageModel model)
        {
            var result = new Message(Guid.NewGuid(), model.chatId, model.senderId, model.content, DateTime.Now);

            await _messageRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetChat(Guid id)
        {
            var response = await _messageRepositories.GetById(id);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            await _messageRepositories.DeleteMessage(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMessage(Guid id, [FromBody] MessageModel model)
        {
            var result = await _messageRepositories.UpdateMessage(new Message(id, model.chatId, model.senderId, model.content, DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Message>>> GetAllMessagesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _messageRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
