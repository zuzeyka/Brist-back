using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[gameGroupController]")]
    public class GameGroupController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameGroupDao _gameGroupDao;

        public GameGroupController(DataContext dataContext, GameGroupDao gameGroupDao)
        {
            _dataContext = dataContext;
            _gameGroupDao = gameGroupDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameGroupDao>>> GetAllGameGroups()
        {
            var gameGroups = await _gameGroupDao.GetAllGameGroups();

            var response = gameGroups.Select(g => new GameGroup(g.id,
                                                                           g.gameId)).ToList();

            return Ok(response);
        }
    }
}
