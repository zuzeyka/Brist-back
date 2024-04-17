using FullStackBrist.Server.Models.Group;
using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly TopicDao _topicDao;

        public TopicController(DataContext dataContext, TopicDao topicDao)
        {
            _dataContext = dataContext;
            _topicDao = topicDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<TopicDao>>> GetAllTopics()
        {
            var topics = await _topicDao.GetAllTopics();

            var response = topics.Select(t => new Topic(id: t.id,
                                                        attachedId: t.attachedId,
                                                        name: t.name,
                                                        description: t.description,
                                                        authorId: t.authorId,
                                                        createdAt: t.createdAt)).ToList();

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<Topic>> CreateTopic([FromBody] TopicModel model)
        {
            var result = new Topic(Guid.NewGuid(),
                model.attachedId,
                model.name,
                model.description,
                model.authorId,
                DateTime.Now);

            var response = _dataContext.dbTopics.AddAsync(result);

            return Ok(response);
        }
    }
}
