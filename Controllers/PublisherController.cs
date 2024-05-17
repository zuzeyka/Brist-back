using FullStackBrist.Server.Models.Creators;
using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CreatorsDao;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Entity.Store.Product.Creators;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly PublisherDao _publisherDao;

        public PublisherController(DataContext dataContext, PublisherDao publisherDao)
        {
            _dataContext = dataContext;
            _publisherDao = publisherDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublisherDao>>> GetAllPublishers()
        {
            var publishers = await _publisherDao.GetAllPublishers();

            var response = publishers.Select(p => new Publisher(id: p.id,
                                                                subscribersCount: p.subscribersCount,
                                                                name: p.name,
                                                                description: p.description,
                                                                avatar: p.avatar,
                                                                backgroundImage: p.backgroundImage,
                                                                urlForNewsPage: p.urlForNewsPage,
                                                                createdAt: p.createdAt)).ToList();

            return Ok(response);
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

            var response = await _dataContext.dbPublishers.AddAsync(result);

            return Ok(response);
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
            var result = new Publisher(id, publisher.subscribersCount, publisher.name, publisher.description, publisher.avatar, publisher.backgroundImage, publisher.urlForNewsPage, publisher.createdAt);
            await _publisherDao.UpdatePublisher(result);
            return NoContent();
        }
    }
}
