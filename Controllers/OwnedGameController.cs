using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileRepository;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnedGameController : Controller
    {
        private readonly OwnedGameRepository _ownedGameDao;

        public OwnedGameController(OwnedGameRepository ownedGameDao)
        {
            _ownedGameDao = ownedGameDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<OwnedGameRepository>>> GetAllOwnedGames()
        {
            var _ownedGames = await _ownedGameDao.GetAllOwnedGames();

            return Ok(_ownedGames);
        }


        [HttpPost]
        public async Task<ActionResult<OwnedGame>> CreateOwnedGame([FromBody] OwnedGameModel model)
        {
            var result = new OwnedGame(Guid.NewGuid(),
                model.ownedGameId,
                model.userId,
                                            DateTime.Now
                                            );
            await _ownedGameDao.Add(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnedGame>> GetOwnedGame(Guid id)
        {
            var response = await _ownedGameDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<OwnedGame>>> GetByUserId(Guid id)
        {
            var response = await _ownedGameDao.GetByUId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}/byuserid/{uid}")]
        public async Task<ActionResult<List<OwnedGame>>> GetUsersByOwnedGame(Guid id, Guid uid)
        {
            var response = await _ownedGameDao.GetByGameId(id, uid);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOwnedGame(Guid id)
        {
            await _ownedGameDao.DeleteOwnedGame(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateOwnedGame(Guid id, [FromBody] OwnedGameModel game)
        {
            var result = await _ownedGameDao.UpdateOwnedGame(new OwnedGame(id, game.ownedGameId, game.userId, game.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<OwnedGame>>> GetAllOwnedGamesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _ownedGameDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
