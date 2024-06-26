using Slush.Entity.Chat;

namespace Slush.Repositories.IRepository
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllMessages(Guid id);
        Task<Message> UpdateMessage(Message message);
        Task Add(Message message);
        Task DeleteMessage(Guid id);
        Task<Message?> GetById(Guid id);
        Task<List<Message?>> GetByIds(List<Guid> id);
    }
}
