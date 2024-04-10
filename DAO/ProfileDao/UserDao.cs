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
            var _userEntities = await _context.dbUsers.AsNoTracking().ToListAsync();

            var _users = _userEntities.Select(u => new User(u.id,
                                                            u.name,
                                                            u.passwordSalt,
                                                            u.salt,
                                                            u.email,
                                                            u.phone)).ToList();
            return _users;
        }
        public void Add(User user)
        {
            _context.dbUsers.Add(user);
            _context.SaveChanges();
        }
    }
}
