using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ChatDao;
using Slush.Entity.Chat;
using Slush.Models.Chat;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly ChatDao _chatDao;

        public ChatController(ChatDao chatDao)
        {
            _chatDao = chatDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatDao>>> GetAllChats()
        {
            var chats = await _chatDao.GetAllChats();

            return Ok(chats);
        }

        [HttpPost]
        public async Task<ActionResult<Chat>> CreateChat([FromBody] ChatModel model)
        {
            var result = new Chat(Guid.NewGuid(), model.firstUser, model.secondUser, DateTime.Now);
            await _chatDao.AddChat(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(Guid id)
        {
            var response = await _chatDao.GetById(id);

            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(Guid id)
        {
            await _chatDao.DeleteChat(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChat(Guid id, [FromBody] ChatModel model)
        {
            var result = await _chatDao.UpdateChat(new Chat(id, model.firstUser, model.secondUser, DateTime.Now));

            return Ok(result);
        }
    }
}
