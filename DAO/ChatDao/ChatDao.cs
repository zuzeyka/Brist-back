using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Chat;

namespace Slush.DAO.ChatDao
{
    public class ChatDao
    {
        private readonly DataContext _context;

        public ChatDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Chat>> GetAllChats()
        {
            return await _context.dbChats
                .Where(c => c.deletedAt == null)
                .Select(c => new Chat
                {
                    id = c.id,
                    firstUser = c.firstUser,
                    secondUser = c.secondUser,
                    createdAt = c.createdAt
                }).ToListAsync();
        }

        public async Task<Chat> UpdateChat(Chat chat)
        {
            var existing = await _context.dbChats.FindAsync(chat.id);
            if (existing != null)
            {
                existing.firstUser = chat.firstUser;
                existing.secondUser = chat.secondUser;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task AddChat(Chat chat)
        {
            await _context.dbChats.AddAsync(chat);
            _context.SaveChanges();
        }

        public async Task DeleteChat(Guid id)
        {
            var requirement = await _context.dbChats.FindAsync(id);
            if (requirement != null)
            {
                requirement.deletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Chat?> GetById(Guid id)
        {
            var response = await _context.dbChats
               .Where(c => c.id == id)
               .Select(c => new Chat
               {
                   id = c.id,
                   firstUser = c.firstUser,
                   secondUser = c.secondUser,
                   createdAt = c.createdAt
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

        public async Task<List<Chat?>> GetByIds(List<Guid> ids)
        {
            List<Chat> response = new List<Chat>();

            foreach (var id in ids)
            {
                var result = await _context.dbChats
               .Where(c => c.id == id)
               .Select(c => new Chat
               {
                   id = c.id,
                   firstUser = c.firstUser,
                   secondUser = c.secondUser,
                   createdAt = c.createdAt
               }).FirstOrDefaultAsync();

                if (result != null)
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
