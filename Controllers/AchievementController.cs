using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileRepository;
using Slush.Entity.Profile;
using Slush.Services.Minio;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementController : Controller
    {
        private readonly AchievementRepository _achievementDao;
        private readonly MinioService _minioService;

        public AchievementController(AchievementRepository achievementDao, MinioService minioService)
        {
            _achievementDao = achievementDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AchievementRepository>>> GetAllAchievements()
        {
            var achievements = await _achievementDao.GetAllAchievements();

            return Ok(achievements);
        }

        [HttpPost]
        public async Task<ActionResult<Achievement>> CreateAchievement([FromBody] Achievement achievement, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            var result = new Achievement(Guid.NewGuid(),
                "",
                achievement.description,
                achievement.amountOfExperience,
                DateTime.Now);

            using (var stream = file.OpenReadStream())
            {
                try
                {
                    String imageUrl = await _minioService.SaveFile("images", result.id, file.FileName, stream);

                    var url = _minioService.GetUrlToFile(imageUrl);

                    result.urlForImage = url.ToString();

                    await _achievementDao.Add(result);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to upload image: {ex.Message}");
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAchievement(Guid id)
        {
            await _achievementDao.Delete(id);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAchievement(Guid id, [FromBody] Achievement achievement, IFormFile file)
        {
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", achievement.id, file.FileName, stream);

                        var url = _minioService.GetUrlToFile(imageUrl);

                        var res = await _achievementDao.UpdateAchievement(new Achievement(id, url.ToString(), achievement.description, achievement.amountOfExperience, achievement.createdAt));

                        return Ok(res);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload image: {ex.Message}");
                    }
                }
            }
            var result = await _achievementDao.UpdateAchievement(new Achievement(id, achievement.urlForImage, achievement.description, achievement.amountOfExperience, achievement.createdAt));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Achievement>> GetAchievement(Guid id)
        {
            var response = await _achievementDao.GetById(id);
            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Achievement>>> GetAllAchievementById([FromBody] List<Guid> guidList)
        {
            var response = await _achievementDao.GetByAllIds(guidList);
            return Ok(response);
        }
    }
}
