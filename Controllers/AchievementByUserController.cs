using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Entity.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementByUserController : Controller
    {
        private readonly AchievementByUserRepository _achievementByUserRepositories;

        public AchievementByUserController(AchievementByUserRepository achievementByUserRepositories)
        {
            _achievementByUserRepositories = achievementByUserRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<AchievementByUserRepository>>> GetAllAchievementsByUser()
        {
            var achievements = await _achievementByUserRepositories.GetAllAchievements();

            return Ok(achievements);
        }

        [HttpPost]
        public async Task<ActionResult<AchievementByUser>> CreateAchievementByUser([FromBody] AchievementByUser achievement)
        {
            var result = new AchievementByUser(Guid.NewGuid(), 
                achievement.userId,
                achievement.achievementId,
                achievement.awardTime,
                DateTime.Now);

            await _achievementByUserRepositories.Add(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAchievement(Guid id)
        {
            await _achievementByUserRepositories.Delete(id);

            return NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateAchievement(Guid id, [FromBody] AchievementByUser achievement)
        {
            var result = await _achievementByUserRepositories.UpdateAchievementByUser(new AchievementByUser(id, achievement.userId, achievement.achievementId, achievement.awardTime, achievement.createdAt));

            return Ok(result);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<AchievementByUser>>> GetAchievementByUserId(Guid id)
        {
            var response = await _achievementByUserRepositories.GetByUserId(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("byid/{id}")]
        public async Task<ActionResult<AchievementByUser>> GetAchievementById(Guid id)
        {
            var response = await _achievementByUserRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<AchievementByUser>>> GetAllAchievementsByUserIds([FromBody] List<Guid> guidList)
        {
            var response = _achievementByUserRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
