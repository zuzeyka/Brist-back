using Slush.Data.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> UpdateUser(User user);
        Task Add(User user);
        Task DeleteUser(Guid id);
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmail(String name);
        Task<User?> GetByUserId(Guid id);
        Task<List<User?>> GetByIds(List<Guid> ids);
    }
}
