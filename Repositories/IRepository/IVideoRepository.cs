using Slush.Data.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IVideoRepository
    {
        Task<List<Video>> GetAllVideos();
        Task<Video> UpdateVideo(Video video);
        Task Add(Video video);
        Task DeleteVideo(Guid id);
        Task<Video?> GetById(Guid id);
        Task<List<Video?>> GetByUId(Guid id);
        Task<List<Video?>> GetByGameId(Guid id);
        Task<List<Video?>> GetByIds(List<Guid> ids);
    }
}
