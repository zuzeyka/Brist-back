using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ChatRepository;
using Slush.Entity.Chat;
using Slush.Models.Chat;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly ChatRepository _ChatRepository;

        public ChatController(ChatRepository ChatRepository)
        {
            _ChatRepository = ChatRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatRepository>>> GetAllChats()
        {
            var chats = await _ChatRepository.GetAllChats();

            return Ok(chats);
        }

        [HttpPost]
        public async Task<ActionResult<Chat>> CreateChat([FromBody] ChatModel model)
        {
            var result = new Chat(Guid.NewGuid(), model.firstUser, model.secondUser, DateTime.Now);
            await _ChatRepository.AddChat(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(Guid id)
        {
            var response = await _ChatRepository.GetById(id);

            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(Guid id)
        {
            await _ChatRepository.DeleteChat(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChat(Guid id, [FromBody] ChatModel model)
        {
            var result = await _ChatRepository.UpdateChat(new Chat(id, model.firstUser, model.secondUser, DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Chat>>> GetAllChatsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _ChatRepository.GetByIds(guidList);

            return Ok(response);
        }
    }
}
