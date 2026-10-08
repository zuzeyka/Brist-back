using Slush.Data;
using Slush.Entity.Profile;
using Microsoft.EntityFrameworkCore;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.ProfileRepository
{
    public class FriendsRepository : IFriendsRepository
    {
        private readonly DataContext _context;

        public FriendsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Friends>> GetAllFriends()
        {
            return await _context.dbFriends
                .Where(f => f.deleteAt == null)
                .Select(f => new Friends {
                id = f.id,
                userId = f.userId,
                friendId = f.friendId,
                createdAt = f.createdAt}).ToListAsync();

        }

        public async Task<Friends> UpdateFriends(Friends friends)
        {
            var existing = await _context.dbFriends.FindAsync(friends.id);
            if (existing != null)
            {
                existing.friendId = friends.friendId;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task<Friends?> Accept(Guid id)
        {
            var existing = await _context.dbFriends.FindAsync(id);
            if (existing != null)
            {
                existing.status = FriendRequestStatus.Accepted;
                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(Friends friend)
        {
            await _context.dbFriends.AddAsync(friend);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFriends(Guid id)
        {
            var requirement = await _context.dbFriends.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Friends?> GetById(Guid id)
        {
            var response = await _context.dbFriends
                .Where(x => x.id == id)
                .Where(x => x.deleteAt == null)
                .Select(f => new Friends
                {
                    id = f.id,
                    userId = f.userId,
                    friendId = f.friendId,
                    createdAt = f.createdAt,
                    status = f.status
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<Friends?>> GetByUserId(Guid id)
        {
            var response = await _context.dbFriends
                .Where(x => x.userId == id)
                .Where(a => a.deleteAt == null)
                .Select(f => new Friends
                {
                    id = f.id,
                    userId = f.userId,
                    friendId = f.friendId,
                    createdAt = f.createdAt,
                    status = f.status
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<Friends?> GetRelationship(Guid userA, Guid userB)
        {
            return await _context.dbFriends
                .Where(f => f.deleteAt == null)
                .Where(f => (f.userId == userA && f.friendId == userB) || (f.userId == userB && f.friendId == userA))
                .Select(f => new Friends
                {
                    id = f.id,
                    userId = f.userId,
                    friendId = f.friendId,
                    createdAt = f.createdAt,
                    status = f.status
                }).FirstOrDefaultAsync();
        }

        public async Task<List<Friends>> GetFriendsOf(Guid userId)
        {
            return await _context.dbFriends
                .Where(f => f.deleteAt == null && f.status == FriendRequestStatus.Accepted)
                .Where(f => f.userId == userId || f.friendId == userId)
                .Select(f => new Friends
                {
                    id = f.id,
                    // Normalized so the caller always sees "the other person" in
                    // friendId, regardless of which side originally sent the request.
                    userId = userId,
                    friendId = f.userId == userId ? f.friendId : f.userId,
                    createdAt = f.createdAt,
                    status = f.status
                }).ToListAsync();
        }

        public async Task<List<Friends?>> GetByUserIds(List<Guid> id)
        {
            List<Friends> response = new List<Friends> ();

            foreach(var i in id)
            {
                var result = await _context.dbFriends
                .Where(x => x.id == i)
                .Where(a => a.deleteAt == null)
                .Select(f => new Friends
                {
                    id = f.id,
                    userId = f.userId,
                    friendId = f.friendId,
                    createdAt = f.createdAt
                }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
