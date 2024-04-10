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
                                                                                s.userId,
                                                                                s.createdAt)).ToList();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<WishedGame>> CreateVideo([FromBody] WishedGameModel model)
        {
            var result = new WishedGame(Guid.NewGuid(),
                model.ownedGameId,
                model.userId,
                DateTime.Now);

            _dataContext.dbWishedGames.AddAsync(result);

            return result;
        }
    }
}
