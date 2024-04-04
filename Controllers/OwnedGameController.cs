using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[ownedGameController]")]
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
                                                                            o.userId)).ToList();
            return Ok(response);
        }
    }
}
