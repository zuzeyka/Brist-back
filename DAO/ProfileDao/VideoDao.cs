using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;

namespace Slush.DAO.ProfileDao
{
    public class VideoDao
    {
        private readonly DataContext _context;

        public VideoDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Video>> GetAllVideos()
        {
            return await _context.dbVideos
                .Where(v => v.deleteAt == null)
                .Select(v => new Video {
                id = v.id,
                title = v.title,
                description = v.description,
                likesCount = v.likesCount,
                dislikesCount = v.dislikesCount,
                gameId = v.gameId,
                authorId = v.authorId,
                videoUrl = v.videoUrl,
                createdAt = v.createdAt}).ToListAsync();

        }
        public void Add(Video video)
        {
            _context.dbVideos.Add(video);
            _context.SaveChanges();
        }

        public async Task DeleteVideo(Guid id)
        {
            var requirement = await _context.dbVideos.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Video> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbVideos.FirstOrDefault(v => v.id == id));
        }
    }
}
