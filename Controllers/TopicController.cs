using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity.Community;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        // Topics are public community content — GET endpoints stay anonymous. Only
        // the author can create/edit/delete their own.
        private readonly ITopicRepository _topicRepositories;

        public TopicController(ITopicRepository topicRepositories)
        {
            _topicRepositories = topicRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<Topic>>> GetAllTopics()
        {
            var topics = await _topicRepositories.GetAllTopics();

            return Ok(topics);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Topic>> CreateTopic([FromBody] TopicModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // A topic can only ever be created under the caller's own authorship;
            // client-supplied model.authorId is ignored to prevent impersonating another user.
            var result = new Topic(Guid.NewGuid(),
                model.attachedId,
                model.name,
                model.description,
                currentUserId.Value,
                DateTime.Now);

            await _topicRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic(Guid id)
        {
            var response = await _topicRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteTopic(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _topicRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.authorId != currentUserId.Value)
            {
                return Forbid();
            }

            await _topicRepositories.DeleteTopic(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateTopic(Guid id, [FromBody] TopicModel topic)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _topicRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.authorId != currentUserId.Value)
            {
                return Forbid();
            }

            // authorId is intentionally not taken from the client — authorship of a
            // topic can never be reassigned to another user.
            var result = await _topicRepositories.UpdateTopic(new Topic(id, topic.attachedId, topic.name, topic.description, existing.authorId, topic.createdAt));
            return Ok(result);
        }

        [HttpGet("byattachedid/{id}")]
        public async Task<ActionResult<List<Topic>>> GetByAttachedId(Guid id)
        {
            var response = await _topicRepositories.GetByAttachedId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Topic>>> GetAllTopicsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _topicRepositories.GetByIds(guidList);

            return Ok(response);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
