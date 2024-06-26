using Slush.Data.Entity.Community;

namespace Slush.Repositories.IRepository
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPosts();
        Task<Post> UpdatePost(Post post);
        Task Add(Post post);
        Task DeletePost(Guid id);
        Task<Post?> GetById(Guid id);
        Task<List<Post?>> GetByAttachedId(Guid id);
        Task<List<Post?>> GetByIds(List<Guid> ids);
    }
}
