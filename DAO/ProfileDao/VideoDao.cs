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
            var _videoEntities = await _context.dbVideos.AsNoTracking().ToListAsync();

            var _videos = _videoEntities.Select(v => new Video(v.id,
                                                                v.title,
                                                                v.description,
                                                                v.likesCount,
                                                                v.dislikesCount,
                                                                v.gameId,
                                                                v.authorId,
                                                                v.videoUrl)).ToList();
            return _videos;

        }
        public void Add(Video video)
        {
            _context.dbVideos.Add(video);
            _context.SaveChanges();
        }
    }
}
