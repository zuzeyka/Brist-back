using Slush.Data.Entity.Community.GameGroup;

namespace Slush.Repositories.IRepository
{
    public interface IGameTopicRepository
    {
        Task<List<GameTopic>> GetAllGameTopics();
        Task<GameTopic> UpdateGameTopic(GameTopic topic);
        Task Add(GameTopic topics);
        Task DeleteGameTopic(Guid id);
        Task<GameTopic?> GetById(Guid id);
        Task<List<GameTopic?>> GetByGameId(Guid id);
        Task<List<GameTopic?>> GetByIds(List<Guid> id);
    }
}
