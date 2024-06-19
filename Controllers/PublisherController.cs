using FullStackBrist.Server.Models.Creators;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CreatorsDao;
using Slush.Entity.Store.Product.Creators;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private readonly PublisherDao _publisherDao;
        private readonly MinioService _minioService;

        public PublisherController(PublisherDao publisherDao, MinioService minioService)
        {
            _publisherDao = publisherDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublisherDao>>> GetAllPublishers()
        {
            var publishers = await _publisherDao.GetAllPublishers();

            return Ok(publishers);
        }
        [HttpPost]
        public async Task<ActionResult<Publisher>> CreatePublisher([FromBody] PublisherModel model, IFormFile avatar, IFormFile background)
        {
            var result = new Publisher(Guid.NewGuid(),
                0,
                model.name,
                model.description,
                model.avatar,
                model.backgroundImage,
                null,
                DateTime.Now);

            if (avatar != null || avatar.Length != 0)
            {
                using (var stream = avatar.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", result.id, avatar.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        result.avatar = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }


            if (background != null || background.Length != 0)
            {
                using (var stream = avatar.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", result.id, background.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        result.backgroundImage = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

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
        public async Task<ActionResult> UpdatePublisher(Guid id, [FromBody] PublisherModel publisher, IFormFile avatar, IFormFile background)
        {
            if (avatar != null || avatar.Length != 0)
            {
                using (var stream = avatar.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, avatar.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        publisher.avatar = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }


            if (background != null || background.Length != 0)
            {
                using (var stream = avatar.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, background.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        publisher.backgroundImage = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            var result = await _publisherDao.UpdatePublisher(new Publisher(id, publisher.subscribersCount, publisher.name, publisher.description, publisher.avatar, publisher.backgroundImage, publisher.urlForNewsPage, publisher.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Publisher>>> GetAllPublishersByIds([FromBody] List<Guid> guidList)
        {
            var response = await _publisherDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
