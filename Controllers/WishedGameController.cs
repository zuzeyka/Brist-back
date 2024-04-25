using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Profile;
using Slush.Entity.Profile;
using System.Net;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishedGameController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly WishedGameDao _wishedGameDao;

        public WishedGameController(DataContext dataContext, WishedGameDao wishedGameDao)
        {
            _dataContext = dataContext;
            _wishedGameDao = wishedGameDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<WishedGameDao>>> GetAllWishedGames()
        {
            var wishedGames = await _wishedGameDao.GetAllWishedGames();

            var response = wishedGames.Select(s => new WishedGame(id: s.id,
                                                                  ownedGameId: s.ownedGameId,
                                                                  userId: s.userId,
                                                                  createdAt: s.createdAt)).ToList();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<WishedGame>> CreateWishedGame([FromBody] WishedGameModel model)
        {
            var result = new WishedGame(Guid.NewGuid(),
                model.ownedGameId,
                model.userId,
                DateTime.Now);

            var response = _dataContext.dbWishedGames.AddAsync(result);

            return Ok(response);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWishedGame(Guid id)
        {
            await _wishedGameDao.DeleteWishedGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWishedGame(Guid id, [FromBody] WishedGameModel game)
        {
            var result = new WishedGame(id, game.ownedGameId, game.userId, game.createdAt);
            await _wishedGameDao.UpdateWishedGame(result);
            return NoContent();
        }
    }
}
