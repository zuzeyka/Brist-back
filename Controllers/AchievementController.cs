using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Entity.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementController : Controller
    {
        private readonly AchievementDao _achievementDao;

        public AchievementController( AchievementDao achievementDao)
        {
            _achievementDao = achievementDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<AchievementDao>>> GetAllAchievements()
        {
            var achievements = await _achievementDao.GetAllAchievements();

            return Ok(achievements);
        }

        [HttpPost]
        public async Task<ActionResult<Achievement>> CreateAchievement([FromBody] Achievement achievement)
        {
            var result = new Achievement(Guid.NewGuid(), 
                achievement.urlForImage,
                achievement.description,
                achievement.amountOfExperience,
                DateTime.Now);

            await _achievementDao.Add(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAchievement(Guid id)
        {
            await _achievementDao.Delete(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAchievement(Guid id, [FromBody] Achievement achievement)
        {
            await _achievementDao.UpdateAchievement(new Achievement(id, achievement.urlForImage, achievement.description, achievement.amountOfExperience, achievement.createdAt));

            return NoContent();
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
    }
}
