using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                                                                           g.gameId,
                                                                           g.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GameGroup>> CreateGroup([FromBody] GameGroupModel model)
        {
            var result = new GameGroup(Guid.NewGuid(),
                                            model.gameId,
                                            DateTime.Now
                                            );
            _dataContext.dbGameGroups.AddAsync(result);

            return result;
        }
    }
}
