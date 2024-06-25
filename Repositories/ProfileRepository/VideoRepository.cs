using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity;

namespace Slush.DAO.ProfileRepository
{
    public class VideoRepository
    {
        private readonly DataContext _context;

        public VideoRepository(DataContext context)
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
                gameId = v.gameId,
                authorId = v.authorId,
                    contentUrl = v.contentUrl,
                createdAt = v.createdAt}).ToListAsync();

        }
        public async Task<Video> UpdateVideo(Video video)
        {
            var existing = await _context.dbVideos.FindAsync(video.id);
            if (existing != null)
            {
                existing.title = video.title;
                existing.description = video.description;
                existing.likesCount = video.likesCount;
                existing.contentUrl = video.contentUrl;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(Video video)
        {
            await _context.dbVideos.AddAsync(video);
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

        public async Task<Video?> GetById(Guid id)
        {
            var response = await _context.dbVideos
                .Where(x => x.id == id)
                .Select(v => new Video
                {
                    id = v.id,
                    title = v.title,
                    description = v.description,
                    likesCount = v.likesCount,
                    gameId = v.gameId,
                    authorId = v.authorId,
                    contentUrl = v.contentUrl,
                    createdAt = v.createdAt
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Video?>> GetByUId(Guid id)
        {
            var response = await _context.dbVideos
                .Where(x => x.authorId == id)
                .Select(v => new Video
                {
                    id = v.id,
                    title = v.title,
                    description = v.description,
                    likesCount = v.likesCount,
                    gameId = v.gameId,
                    authorId = v.authorId,
                    contentUrl = v.contentUrl,
                    createdAt = v.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Video?>> GetByGameId(Guid id)
        {
            var response = await _context.dbVideos
                .Where(x => x.gameId == id)
                .Select(v => new Video
                {
                    id = v.id,
                    title = v.title,
                    description = v.description,
                    likesCount = v.likesCount,
                    gameId = v.gameId,
                    authorId = v.authorId,
                    contentUrl = v.contentUrl,
                    createdAt = v.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Video?>> GetByIds(List<Guid> ids)
        {
            List<Video> response = new List<Video> ();

            foreach(var id in ids)
            {
                var result = await _context.dbVideos
                    .Where(x => x.id == id)
                    .Select(v => new Video
                    {
                        id = v.id,
                        title = v.title,
                        description = v.description,
                        likesCount = v.likesCount,
                        gameId = v.gameId,
                        authorId = v.authorId,
                        contentUrl = v.contentUrl,
                        createdAt = v.createdAt
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
            else
            {
                return null;
            }
        }

    }
}
