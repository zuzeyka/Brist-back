using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Entity.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementByUserController : Controller
    {
        private readonly AchievementByUserDao _achievementByUserDao;

        public AchievementByUserController(AchievementByUserDao achievementByUserDao)
        {
            _achievementByUserDao = achievementByUserDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<AchievementByUserDao>>> GetAllAchievementsByUser()
        {
            var achievements = await _achievementByUserDao.GetAllAchievements();

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

            await _achievementByUserDao.Add(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAchievement(Guid id)
        {
            await _achievementByUserDao.Delete(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAchievement(Guid id, [FromBody] AchievementByUser achievement)
        {
            var result = await _achievementByUserDao.UpdateAchievementByUser(new AchievementByUser(id, achievement.userId, achievement.achievementId, achievement.awardTime, achievement.createdAt));

            return Ok(result);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<AchievementByUser>>> GetAchievementByUserId(Guid id)
        {
            var response = await _achievementByUserDao.GetByUserId(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("byid/{id}")]
        public async Task<ActionResult<AchievementByUser>> GetAchievementById(Guid id)
        {
            var response = await _achievementByUserDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<AchievementByUser>>> GetAllAchievementsByUserIds([FromBody] List<Guid> guidList)
        {
            var response = _achievementByUserDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
