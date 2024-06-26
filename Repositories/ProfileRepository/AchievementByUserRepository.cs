using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;

namespace Slush.Repositories.ProfileRepository
{
    public class AchievementByUserRepository
    {
        private readonly DataContext _context;

        public AchievementByUserRepository(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<List<AchievementByUser>> GetAllAchievements()
        {
            return await _context.dbAchievementByUser
                .Where(c => c.deletedAt == null)
                .Select(c => new AchievementByUser
                {
                    id = c.id,
                    userId = c.userId,
                    achievementId = c.achievementId,
                    awardTime = c.awardTime,
                    createdAt = c.createdAt
                }).ToListAsync();
        }

        public async Task<AchievementByUser> UpdateAchievementByUser(AchievementByUser achievementByUser)
        {
            var existing = await _context.dbAchievementByUser.FindAsync(achievementByUser.id);

            if(existing != null)
            {
                existing.userId = achievementByUser.userId;
                existing.achievementId = achievementByUser.achievementId;
                existing.awardTime = achievementByUser.awardTime;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(AchievementByUser achievementByUser)
        {
            await _context.dbAchievementByUser.AddAsync(achievementByUser);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var existing = await _context.dbAchievementByUser.FindAsync(id);

            if(existing != null)
            {
                existing.deletedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<AchievementByUser?> GetById(Guid id)
        {
            var response = await _context.dbAchievementByUser
                .Where(x => x.userId == id)
                .Select(x => new AchievementByUser { 
                    id = x.id,
                    userId = x.userId,
                    achievementId = x.achievementId,
                    awardTime = x.awardTime}).FirstOrDefaultAsync();
            if(response != null)
            {
                return response;
            }
            else { return null; }
        }

        public async Task<List<AchievementByUser?>> GetByUserId(Guid id)
        {
            var response = await _context.dbAchievementByUser
                .Where(x => x.userId == id)
                .Where(c => c.deletedAt == null)
                .Select(x => new AchievementByUser
                {
                    id = x.id,
                    userId = x.userId,
                    achievementId = x.achievementId,
                    awardTime = x.awardTime,
                    createdAt = x.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else { return null; }
        }

        public async Task<List<AchievementByUser?>> GetByIds(List<Guid> guidList)
        {
            List<AchievementByUser> response = new List<AchievementByUser> ();

            foreach(var item in guidList)
            {
                var result = await _context.dbAchievementByUser
                    .Where(x => x.id == item)
                    .Where(c => c.deletedAt == null)
                    .Select(x => new AchievementByUser
                    {
                        id = x.id,
                        userId = x.userId,
                        achievementId = x.achievementId,
                        awardTime = x.awardTime,
                        createdAt = x.createdAt
                    }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }

            if (response != null)
            {
                return response;
            }
            else { return null; }
        }
    }
}
