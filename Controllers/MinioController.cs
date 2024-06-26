using Microsoft.AspNetCore.Mvc;
using Minio;
using Slush.Services.Minio;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MinioController : Controller
    {
        private readonly IMinioClient _minioClient;
        private readonly IMinioService _minioService;

        private readonly ILogger<MinioController> _logger;

        public MinioController(IMinioClient minioClient, ILogger<MinioController> logger, IMinioService minioService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _minioClient = minioClient;
            _minioService = minioService;
        }

        [HttpGet("buckets")]
        public async Task<ActionResult> ListBuckets()
        {
            return Ok(await _minioService.ListBuckets());
        }

        [HttpGet("files/{bucketName}")]
        public async Task<ActionResult> GetFiles(String bucketName)
        {
            return Ok(await _minioService.ListBucketFiles(bucketName));
        }

        [HttpPost("upload/{attachedId}")]
        public async Task<ActionResult> UploadImage(Guid attachedId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }
            _logger.LogWarning(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                try
                {
                    String imageUrl = await _minioService.SaveFile("images", attachedId, file.FileName, stream);
                    
                    var url = await _minioService.GetUrlToFile(imageUrl);

                    return Ok(url.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    return StatusCode(500, $"Failed to upload file: {ex.Message}");
                }
            }
        }

        [HttpGet("geturl/{fileName}")]
        public async Task<ActionResult> GetUrlToImage(String fileName)
        {
            return Ok(await _minioService.GetUrlToFile(fileName));
        }

        [HttpGet("geturltofiles")]
        public async Task<ActionResult> GetUrlToImages()
        {
            return Ok(await _minioService.GetUrlToFiles());
        }
    }
}
