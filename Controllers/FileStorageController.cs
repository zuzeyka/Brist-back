using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Services.FileStorage;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileStorageController : Controller
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<FileStorageController> _logger;

        public FileStorageController(ILogger<FileStorageController> logger, IFileStorageService fileStorageService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileStorageService = fileStorageService;
        }

        [HttpPost("upload/{attachedId}")]
        public async Task<ActionResult> UploadImage(Guid attachedId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            using (var stream = file.OpenReadStream())
            {
                try
                {
                    String fileKey = await _fileStorageService.SaveFile("images", attachedId, file.FileName, stream);

                    var url = await _fileStorageService.GetUrlToFile(fileKey);

                    return Ok(url);
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    return StatusCode(500, $"Failed to upload file: {ex.Message}");
                }
            }
        }

        [HttpGet("geturl/{*fileKey}")]
        public async Task<ActionResult> GetUrlToImage(String fileKey)
        {
            return Ok(await _fileStorageService.GetUrlToFile(fileKey));
        }
    }
}
