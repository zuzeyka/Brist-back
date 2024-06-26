using Slush.Data.Entity.Community.GameGroup;

namespace Slush.Repositories.IRepository
{
    public interface IGameCommentRepository
    {
        Task<List<GameComment>> GetAllGameComments();
        Task<GameComment> UpdateGameComment(GameComment comment);
        Task Add(GameComment comment);
        Task DeleteGameComment(Guid id);
        Task<GameComment?> GetById(Guid id);
        Task<List<GameComment?>> GetByGamePostId(Guid id);
        Task<List<GameComment?>> GetByIds(List<Guid> id);
    }
}
