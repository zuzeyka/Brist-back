using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.dbVideos.Select(v => new Video {
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
    }
}
