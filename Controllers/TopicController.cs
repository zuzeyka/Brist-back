using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private readonly TopicDao _topicDao;

        public TopicController(TopicDao topicDao)
        {
            _topicDao = topicDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<TopicDao>>> GetAllTopics()
        {
            var topics = await _topicDao.GetAllTopics();

            return Ok(topics);
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

            await _topicDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic(Guid id)
        {
            var response = await _topicDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTopic(Guid id)
        {
            await _topicDao.DeleteTopic(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateTopic(Guid id, [FromBody] TopicModel topic)
        {
            var result = await _topicDao.UpdateTopic(new Topic(id, topic.attachedId, topic.name, topic.description, topic.authorId, topic.createdAt));
            return Ok(result);
        }

        [HttpGet("byattachedid/{id}")]
        public async Task<ActionResult<List<Topic>>> GetByAttachedId(Guid id)
        {
            var response = await _topicDao.GetByAttachedId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Topic>>> GetAllTopicsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _topicDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
