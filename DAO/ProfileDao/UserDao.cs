using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.dbUsers.Select(u => new User {
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
    }
}
