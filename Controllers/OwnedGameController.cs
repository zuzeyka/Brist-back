using FullStackBrist.Server.Models.Profile;
using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
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

            var response = _ownedGames.Select(o => new OwnedGame(o.id,
                                                                            o.ownedGameId,
                                                                            o.userId,
                                                                            o.createdAt)).ToList();
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
            _dataContext.dbOwnedGames.AddAsync(result);
            return Ok(result);
        }
    }
}
