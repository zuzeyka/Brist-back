using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data.Entity.Profile;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishedGameController : Controller
    {
        private readonly WishedGameDao _wishedGameDao;

        public WishedGameController(WishedGameDao wishedGameDao)
        {
            _wishedGameDao = wishedGameDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<WishedGameDao>>> GetAllWishedGames()
        {
            var wishedGames = await _wishedGameDao.GetAllWishedGames();

            return Ok(wishedGames);
        }


        [HttpPost]
        public async Task<ActionResult<WishedGame>> CreateWishedGame([FromBody] WishedGameModel model)
        {
            var result = new WishedGame(Guid.NewGuid(),
                model.ownedGameId,
                model.userId,
                DateTime.Now);

            await _wishedGameDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetWishedGame(Guid id)
        {
            var response = await _wishedGameDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("byuid/{id}/bygameid/{gameid}")]
        public async Task<ActionResult<User>> GetWishedGameByUser(Guid id, Guid gameid)
        {
            var response = await _wishedGameDao.GetByUserAndGameId(id, gameid);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWishedGame(Guid id)
        {
            await _wishedGameDao.DeleteWishedGame(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateWishedGame(Guid id, [FromBody] WishedGameModel game)
        {
            var result = await _wishedGameDao.UpdateWishedGame(new WishedGame(id, game.ownedGameId, game.userId, game.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<WishedGame>>> GetAllWishedGamesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _wishedGameDao.GetIds(guidList);

            return Ok(response);
        }
    }
}
