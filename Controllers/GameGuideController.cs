using FullStackBrist.Server.Models.GameGroup;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameGuideController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameGuideDao _gameGuideDao;

        public GameGuideController(DataContext dataContext, GameGuideDao gameGuideDao)
        {
            _dataContext = dataContext;
            _gameGuideDao = gameGuideDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameGuideDao>>> GetAllGameGuides()
        {
            var _gameGuides = await _gameGuideDao.GetAllGameGuides();

            var response = _gameGuides.Select(g => new GameGuide(id: g.id,
                                                                 title: g.title,
                                                                 description: g.description,
                                                                 likesCount: g.likesCount,
                                                                 discussionId: g.discussionId,
                                                                 gameId: g.gameId,
                                                                 authorId: g.authorId,
                                                                 gameGroupId: g.gameGroupId,
                                                                 content: g.content,
                                                                 createdAt: g.createdAt)).ToList();

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<GameGuide>> CreateGuide([FromBody] GameGuideModel model)
        {
            var result = new GameGuide(Guid.NewGuid(),
                                            model.title,
                                            model.description,
                                            0,
                                            model.discussionId,
                                            model.gameId,
                                            model.authorId,
                                            model.gameGroupId,
                                            model.content,
                                            DateTime.Now
                                            );
            var response = _dataContext.dbGameGuides.AddAsync(result);

            return Ok(response);
        }
    }
}
