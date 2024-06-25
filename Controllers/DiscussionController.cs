using Microsoft.AspNetCore.Mvc;
using Slush.Repositories;
using Slush.Entity;
using Slush.Models;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionController : Controller
    {
        private readonly DiscussionRepository _DiscussionRepository;

        public DiscussionController( DiscussionRepository DiscussionRepository)
        {
            _DiscussionRepository = DiscussionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiscussionRepository>>> GetAllDiscussions()
        {
            var _discussions = await _DiscussionRepository.GetAllDiscussions();

            return Ok(_discussions);
        }

        [HttpPost]
        public async Task<ActionResult<Discussion>> CreateDiscussion([FromBody] DiscussionModel model)
        {
            var result = new Discussion(Guid.NewGuid(),
                model.authordId,
                model.attachedId,
                model.content,
                model.likesCount,
                model.rate,
                DateTime.Now);

            var response = _DiscussionRepository.UpdateDiscussion(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discussion>> GetDiscussion(Guid id)
        {
            var response = await _DiscussionRepository.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("byattachedid/{id}")]
        public async Task<ActionResult<List<Discussion>>> GetDiscussionByAttachedId(Guid id)
        {
            var response = await _DiscussionRepository.GetByAttachedId(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiscussion(Guid id)
        {
            await _DiscussionRepository.DeleteDiscussion(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateDiscussion(Guid id, [FromBody] DiscussionModel model)
        {
            var result = await _DiscussionRepository.UpdateDiscussion(new Discussion(id, model.authordId, model.attachedId, model.content, model.likesCount, model.rate, model.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Discussion>>> GetAllDiscussionsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _DiscussionRepository.GetByIds(guidList);

            return Ok(response);
        }
    }
}
