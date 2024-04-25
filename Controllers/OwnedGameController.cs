using FullStackBrist.Server.Models.Profile;
using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.DAO.RequirementsDao;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnedGameController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly OwnedGameDao _ownedGameDao;

        public OwnedGameController(DataContext dataContext, OwnedGameDao ownedGameDao)
        {
            _dataContext = dataContext;
            _ownedGameDao = ownedGameDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<OwnedGameDao>>> GetAllOwnedGames()
        {
            var _ownedGames = await _ownedGameDao.GetAllOwnedGames();

            var response = _ownedGames.Select(o => new OwnedGame(id: o.id,
                                                                 ownedGameId: o.ownedGameId,
                                                                 userId: o.userId,
                                                                 createdAt: o.createdAt)).ToList();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<OwnedGame>> CreateOwnedGame([FromBody] OwnedGameModel model)
        {
            var result = new OwnedGame(Guid.NewGuid(),
                model.ownedGameId,
                model.userId,
                                            DateTime.Now
                                            );
            var response = _dataContext.dbOwnedGames.AddAsync(result);
            return Ok(response);

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOwnedGame(Guid id)
        {
            await _ownedGameDao.DeleteOwnedGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMinimalSystemRequirement(Guid id, [FromBody] OwnedGameModel game)
        {
            var result = new OwnedGame(id, game.ownedGameId, game.userId, game.createdAt);
            await _ownedGameDao.UpdateOwnedGame(result);
            return NoContent();
        }
    }
}
