using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[topicController]")]
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

            var response = topics.Select(t => new Topic(t.id,
                                                                t.attachedId,
                                                                t.name,
                                                                t.description,
                                                                t.authorId)).ToList();

            return Ok(response);
        }
    }
}
