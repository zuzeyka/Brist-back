using Slush.Data.Entity.Community.GameGroup;

namespace Slush.Repositories.IRepository
{
    public interface IGamePostsRepository
    {
        Task<List<GamePosts>> GetAllGamePosts();
        Task<GamePosts> UpdateGamePosts(GamePosts post);
        Task Add(GamePosts posts);
        Task DeleteGamePosts(Guid id);
        Task<GamePosts?> GetById(Guid id);
        Task<List<GamePosts?>> GetByGameId(Guid id);
        Task<List<GamePosts?>> GetByIds(List<Guid> id);
    }
}
