using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CreatorsDao;
using Slush.Data;
using Slush.Entity.Store.Product.Creators;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[publisherController]")]
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

            var response = publishers.Select(p => new Publisher(p.id,
                                                                           p.subscribersCount,
                                                                           p.name,
                                                                           p.description,
                                                                           p.avatar,
                                                                           p.backgroundImage,
                                                                           p.urlForNewsPage)).ToList();

            return Ok(response);
        }
    }
}
