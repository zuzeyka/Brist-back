using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ChatDao;
using Slush.Entity.Chat;
using Slush.Models.Chat;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly MessageDao _messageDao;

        public MessageController(MessageDao messageDao)
        {
            _messageDao = messageDao;
        }

        [HttpGet("bychat/{id}")]
        public async Task<ActionResult<List<MessageDao>>> GetAllMessages([FromBody] Chat chat)
        {
            var messages = await _messageDao.GetAllMessages(chat.id);

            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage([FromBody] MessageModel model)
        {
            var result = new Message(Guid.NewGuid(), model.chatId, model.senderId, model.content, DateTime.Now);

            await _messageDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetChat(Guid id)
        {
            var response = await _messageDao.GetById(id);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            await _messageDao.DeleteMessage(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMessage(Guid id, [FromBody] MessageModel model)
        {
            var result = await _messageDao.UpdateMessage(new Message(id, model.chatId, model.senderId, model.content, DateTime.Now));

            return Ok(result);
        }
    }
}
