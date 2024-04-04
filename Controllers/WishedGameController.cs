using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Entity.Profile;
using System.Net;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[wishedGameController]")]
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

            var response = wishedGames.Select(s => new WishedGame(s.id,
                                                                                s.ownedGameId,
                                                                                s.userId)).ToList();
            return Ok(response);
        }
    }
}
