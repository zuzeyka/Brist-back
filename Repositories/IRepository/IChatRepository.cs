using Slush.Entity.Chat;

namespace Slush.Repositories.IRepository
{
    public interface IChatRepository
    {
        Task<List<Chat>> GetAllChats();
        Task<Chat> UpdateChat(Chat chat);
        Task AddChat(Chat chat);
        Task DeleteChat(Guid id);
        Task<Chat?> GetById(Guid id);
        Task<List<Chat?>> GetByIds(List<Guid> ids);
    }
}
