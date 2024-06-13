using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileDao
{
    public class AchievementDao
    {
        private readonly DataContext _dataContext;

        public AchievementDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Achievement>> GetAllAchievements()
        {
            return await _dataContext.dbAchievements
                .Where(a => a.deletedAt == null)
                .Select(a => new Achievement
                {
                    id = a.id,
                    urlForImage = a.urlForImage,
                    description = a.description,
                    amountOfExperience = a.amountOfExperience,
                    createdAt = a.createdAt,
                }).ToListAsync();
        }

        public async Task UpdateAchievement(Achievement achievement)
        {
            var existing = await _dataContext.dbAchievements.FindAsync(achievement.id);

            if(existing != null)
            {
                existing.description = achievement.description;
                existing.urlForImage = achievement.urlForImage;
                existing.amountOfExperience = achievement.amountOfExperience;

                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Add(Achievement achievement)
        {
            await _dataContext.dbAchievements.AddAsync(achievement);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var existing = await _dataContext.dbAchievements.FindAsync(id);

            if (existing != null)
            {
                existing.deletedAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<Achievement?> GetById(Guid id)
        {
            var response = await _dataContext.dbAchievements
                .Where(x => x.id == id)
                .Select(x => new Achievement
                {
                    id = x.id,
                    urlForImage = x.urlForImage,
                    description = x.description,
                    amountOfExperience = x.amountOfExperience,
                    createdAt = x.createdAt
                }).FirstOrDefaultAsync();

            if(response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
