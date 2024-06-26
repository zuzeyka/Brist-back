using FullStackBrist.Server.Models.Creators;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Store.Product.Creators;
using Slush.Services.Minio;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : Controller
    {
        private readonly IDeveloperRepository _developerRepositories;
        private readonly IMinioService _minioService;

        public DeveloperController(IDeveloperRepository developerRepositories, IMinioService minioService)
        {
            _developerRepositories = developerRepositories;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IDeveloperRepository>>> GetAllDevelopers()
        {
            var _developers = await _developerRepositories.GetAllDevelopersRepositories();

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

            await _developerRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(Guid id)
        {
            var response = await _developerRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeveloper(Guid id)
        {
            await _developerRepositories.DeleteDeveloper(id);
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

            var result = await _developerRepositories.UpdateDeveloper(new Developer(id, model.subscribersCount, model.name, model.description, model.avatar, model.backgroundImage, model.urlForNewsPage, model.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Developer>>> GetAllDevelopersByIds(List<Guid> guidList)
        {
            var result = await _developerRepositories.GetByIds(guidList);

            return Ok(result);
        }
    }
}
