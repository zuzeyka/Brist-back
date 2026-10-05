using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity;
using FullStackBrist.Server.Models.Categories;
using Slush.Services.FileStorage;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesByAuthorController : Controller
    {
        private readonly ICategoriesByAuthorRepository _categoriesByAuthorRepositories;
        private readonly IFileStorageService _fileStorageService;

        public CategoriesByAuthorController(ICategoriesByAuthorRepository categoriesByAuthorRepositories, IFileStorageService fileStorageService)
        {
            _categoriesByAuthorRepositories = categoriesByAuthorRepositories;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ICategoriesByAuthorRepository>>> GetAllCategoriesByAuthor()
        {
            var _categoriesByAuthor = await _categoriesByAuthorRepositories.GetAllCategoriesByAuthor();

            return Ok(_categoriesByAuthor);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryByAuthor>> CreateCategoryByAuthor([FromBody] CategoryByAuthorModel model, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            var result = new CategoryByAuthor(Guid.NewGuid(),
                                        model.authorId,
                                        model.name,
                                        model.description,
                                        model.image,
                                        DateTime.Now);

            using (var stream = file.OpenReadStream())
            {
                try
                {
                    String imageUrl = await _fileStorageService.SaveFile("images", result.id, file.FileName, stream);

                    var url = _fileStorageService.GetUrlToFile(imageUrl);

                    result.image = url.ToString();

                    await _categoriesByAuthorRepositories.Add(result);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to upload image: {ex.Message}");
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryByAuthor>> GetCategoryByAuthor(Guid id)
        {
            var response = await _categoriesByAuthorRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryByAuthor(Guid id)
        {
            await _categoriesByAuthorRepositories.DeleteCategoryByAuthor(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryByAuthor(Guid id, [FromBody] CategoryByAuthorModel model, IFormFile file)
        {
            if (file != null && file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _fileStorageService.SaveFile("images", id, file.FileName, stream);

                        var url = _fileStorageService.GetUrlToFile(imageUrl);

                        model.image = url.ToString();
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload image: {ex.Message}");
                    }
                }
            }

            var result = await _categoriesByAuthorRepositories.UpdateCategoriesByAuthor(new CategoryByAuthor(id, model.authorId, model.name, model.description, model.image, DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<CategoryByAuthor>>> GetAllCategoriesByIds([FromBody] List<Guid> guidlist)
        {
            var response = await _categoriesByAuthorRepositories.GetAllCategoriesByIds(guidlist);

            return Ok(response);
        }
    }
}
