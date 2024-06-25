using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupRepository;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameGroupController : Controller
    {
        private readonly GameGroupRepository _GameGroupRepository;

        public GameGroupController(GameGroupRepository GameGroupRepository)
        {
            _GameGroupRepository = GameGroupRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameGroupRepository>>> GetAllGameGroups()
        {
            var gameGroups = await _GameGroupRepository.GetAllGameGroups();

            return Ok(gameGroups);
        }

        [HttpPost]
        public async Task<ActionResult<GameGroup>> CreateGroup([FromBody] GameGroupModel model)
        {
            var result = new GameGroup(Guid.NewGuid(),
                                            model.gameId,
                                            DateTime.Now
                                            );
            await _GameGroupRepository.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameGroup>> GetGameGroup(Guid id)
        {
            var response = await _GameGroupRepository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameGroup(Guid id)
        {
            await _GameGroupRepository.DeleteGameGroup(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateGameGroup(Guid id, [FromBody] GameGroupModel game)
        {
            var result = await _GameGroupRepository.UpdateGameGroup(new GameGroup(id, game.gameId, game.createdAt));
            return Ok(result);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameGroup>>> GetByGameId(Guid id)
        {
            var response = await _GameGroupRepository.GetByGameId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameGroup>>> GetAllGameGroupsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _GameGroupRepository.GetByIds(guidList);

            return Ok(response);
        }
    }
}
