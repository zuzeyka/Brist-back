using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;

namespace Slush.DAO.ProfileDao
{
    public class UserDao
    {
        private readonly DataContext _context;

        public UserDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.dbUsers
                .Where(u => u.deleteAt == null)
                .Select(u => new User {
                id = u.id,
                name = u.name,
                passwordSalt = u.passwordSalt,
                salt = u.salt,
                email = u.email,
                phone = u.phone,
                createdAt = u.createdAt}).ToListAsync();
        }
        public void Add(User user)
        {
            _context.dbUsers.Add(user);
            _context.SaveChanges();
        }

        public async Task DeleteUser(Guid id)
        {
            var requirement = await _context.dbUsers.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbUsers.FirstOrDefault(u => u.id == id));
        }
    }
}
