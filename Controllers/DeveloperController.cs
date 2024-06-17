using FullStackBrist.Server.Models.Creators;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Slush.DAO.CreatorsDao;
using Slush.Entity.Store.Product.Creators;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : Controller
    {
        private readonly DeveloperDao _developerDao;
        private readonly MinioService _minioService;

        public DeveloperController(DeveloperDao developerDao, MinioService minioService)
        {
            _developerDao = developerDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeveloperDao>>> GetAllDevelopers()
        {
            var _developers = await _developerDao.GetAllDevelopersDao();

            return Ok(_developers);
        }

        [HttpPost]
        public async Task<ActionResult<Developer>> CreateDeveloper([FromBody] DeveloperModel model, IFormFile avatar, IFormFile background)
        {

            if (avatar == null || avatar.Length == 0 &&
                background == null || background.Length == 0)
            {
                return BadRequest("File is empty");
            }

            var result =  new Developer(Guid.NewGuid(),
                                        0,
                                        model.name,
                                        model.description,
                                        model.avatar,
                                        model.backgroundImage,
                                        null,
                                        DateTime.Now);

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
                    return StatusCode(500, $"Failed to upload image: {ex.Message}");
                }
            }

            using (var stream = background.OpenReadStream())
            {
                try
                {
                    String imageUrl = await _minioService.SaveFile("images", result.id, background.FileName, stream);

                    var url = await _minioService.GetUrlToFile(imageUrl);

                    result.backgroundImage = url;
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to upload image: {ex.Message}");
                }
            }

            await _developerDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(Guid id)
        {
            var response = await _developerDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeveloper(Guid id)
        {
            await _developerDao.DeleteDeveloper(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDeveloper(Guid id, [FromBody] DeveloperModel model, IFormFile avatar, IFormFile background)
        {
            if (avatar != null || avatar.Length != 0)
            {
                using (var stream = avatar.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, avatar.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        model.avatar = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload image: {ex.Message}");
                    }
                }
            }
            if(background != null || background.Length != 0)
            {
                using (var stream = background.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, background.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        model.backgroundImage = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload image: {ex.Message}");
                    }
                }
            }

            var result = await _developerDao.UpdateDeveloper(new Developer(id, model.subscribersCount, model.name, model.description, model.avatar, model.backgroundImage, model.urlForNewsPage, model.createdAt));
            return Ok(result);
        }
    }
}
