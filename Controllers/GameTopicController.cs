using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[gameTopicController]")]
    public class GameTopicController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameTopicDao _gameTopicDao;

        public GameTopicController(DataContext dataContext, GameTopicDao gameTopicDao)
        {
            _dataContext = dataContext;
            _gameTopicDao = gameTopicDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameTopicDao>>> GetAllGameTopics()
        {
            var gameTopics = await _gameTopicDao.GetAllGameTopics();

            var response = gameTopics.Select(g => new GameTopic(g.id,
                                                                            g.attachedId,
                                                                            g.name,
                                                                            g.description)).ToList();
            return Ok(response);
        }
    }
}
