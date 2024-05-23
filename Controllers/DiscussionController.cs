using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Utilities;
using Slush.DAO;
using Slush.Data;
using Slush.Entity;
using Slush.Models;

namespace Slush.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class DiscussionController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly DiscussionDao _discussionDao;

        public DiscussionController(DataContext dataContext, DiscussionDao discussionDao)
        {
            _dataContext = dataContext;
            _discussionDao = discussionDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiscussionDao>>> GetAllDiscussions()
        {
            var _discussions = await _discussionDao.GetAllDiscussions();

            var response = _discussions.Select(d => new Discussion(
                id: d.id,
                authorId: d.authorId,
                attachedId: d.attachedId,
                content: d.content,
                likesCount: d.likesCount,
                createdAt: d.createdAt
                )).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Discussion>> CreateDiscussion([FromBody] DiscussionModel model)
        {
            var result = new Discussion(Guid.NewGuid(),
                model.authordId,
                model.attachedId,
                model.content,
                model.likesCount,
                DateTime.Now);

            var response = _discussionDao.UpdateDiscussion(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discussion>> GetDiscussion(Guid id)
        {
            var response = await _discussionDao.GetById(id);

            if (response != null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiscussion(Guid id)
        {
            await _discussionDao.DeleteDiscussion(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDiscussion(Guid id, [FromBody] DiscussionModel model)
        {
            var result = new Discussion(id, model.authordId, model.attachedId, model.content, model.likesCount, model.createdAt);
            await _discussionDao.UpdateDiscussion(result);
            return NoContent();
        }
    }
}
