using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ChatDao;
using Slush.Data;
using Slush.Entity.Chat;
using Slush.Models.Chat;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly MessageDao _messageDao;

        public MessageController(DataContext dataContext, MessageDao messageDao)
        {
            _dataContext = dataContext;
            _messageDao = messageDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<MessageDao>>> GetAllMessages([FromBody] Chat chat)
        {
            var messages = await _messageDao.GetAllMessages(chat.id);

            var response = messages.Select(m => new Message(id: m.id,
                chatId: m.chatId,
                senderId: m.senderId,
                content: m.content,
                createdAt: m.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage([FromBody] MessageModel model)
        {
            var result = new Message(Guid.NewGuid(), model.id, model.senderId, model.content, DateTime.Now);

            var response = await _dataContext.dbMessages.AddAsync(result);

            return Ok(response);
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
            var result = new Message(id, model.chatId, model.senderId, model.content, DateTime.Now);

            await _messageDao.UpdateMessage(result);

            return NoContent();
        }
    }
}
