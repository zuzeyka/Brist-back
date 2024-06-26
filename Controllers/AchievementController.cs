using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Entity.Profile;
using Slush.Services.Minio;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementController : Controller
    {
        private readonly IAchievementRepository _achievementRepositories;
        private readonly IMinioService _minioService;

        public AchievementController(IAchievementRepository achievementRepositories, IMinioService minioService)
        {
            _achievementRepositories = achievementRepositories;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AchievementRepository>>> GetAllAchievements()
        {
            var achievements = await _achievementRepositories.GetAllAchievements();

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

                    await _achievementRepositories.Add(result);

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
            await _achievementRepositories.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
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

                        var res = await _achievementRepositories.UpdateAchievement(new Achievement(id, url.ToString(), achievement.description, achievement.amountOfExperience, achievement.createdAt));

                        return Ok(res);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload image: {ex.Message}");
                    }
                }
            }
            var result = await _achievementRepositories.UpdateAchievement(new Achievement(id, achievement.urlForImage, achievement.description, achievement.amountOfExperience, achievement.createdAt));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Achievement>> GetAchievement(Guid id)
        {
            var response = await _achievementRepositories.GetById(id);
            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Achievement>>> GetAllAchievementById([FromBody] List<Guid> guidList)
        {
            var response = await _achievementRepositories.GetByAllIds(guidList);
            return Ok(response);
        }
    }
}
