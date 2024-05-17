using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ChatDao;
using Slush.Data;
using Slush.Entity.Chat;
using Slush.Models.Chat;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ChatDao _chatDao;

        public ChatController(DataContext dataContext, ChatDao chatDao)
        {
            _dataContext = dataContext;
            _chatDao = chatDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatDao>>> GetAllChats()
        {
            var chats = await _chatDao.GetAllChats();
            var response = chats.Select(c => new Chat(id: c.id,
                firstUser: c.firstUser,
                secondUser: c.secondUser,
                createdAt: c.createdAt));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Chat>> CreateChat([FromBody] ChatModel model)
        {
            var result = new Chat(Guid.NewGuid(), model.firstUser, model.secondUser, DateTime.Now);
            var response =  await _dataContext.dbChats.AddAsync(result);

            return Ok(response);
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
            var result = new Chat(id, model.firstUser, model.secondUser, DateTime.Now);

            await _chatDao.UpdateChat(result);

            return NoContent();
        }
    }
}
