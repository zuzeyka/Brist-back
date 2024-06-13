using FullStackBrist.Server.Models.Creators;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CreatorsDao;
using Slush.Entity.Store.Product.Creators;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private readonly PublisherDao _publisherDao;

        public PublisherController(PublisherDao publisherDao)
        {
            _publisherDao = publisherDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublisherDao>>> GetAllPublishers()
        {
            var publishers = await _publisherDao.GetAllPublishers();

            return Ok(publishers);
        }
        [HttpPost]
        public async Task<ActionResult<Publisher>> CreatePublisher([FromBody] PublisherModel model)
        {
            var result = new Publisher(Guid.NewGuid(),
                0,
                model.name,
                model.description,
                model.avatar,
                model.backgroundImage,
                null,
                DateTime.Now);

            await _publisherDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(Guid id)
        {
            var response = await _publisherDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublisher(Guid id)
        {
            await _publisherDao.DeletePublisher(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePublisher(Guid id, [FromBody] PublisherModel publisher)
        {
            await _publisherDao.UpdatePublisher(new Publisher(id, publisher.subscribersCount, publisher.name, publisher.description, publisher.avatar, publisher.backgroundImage, publisher.urlForNewsPage, publisher.createdAt));
            return NoContent();
        }
    }
}
