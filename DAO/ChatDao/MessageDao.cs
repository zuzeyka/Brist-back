using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using Slush.Data;
using Slush.Entity.Chat;

namespace Slush.DAO.ChatDao
{
    public class MessageDao
    {
        private readonly DataContext _context;

        public MessageDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAllMessages(Guid id)
        {
            return await _context.dbMessages
                .Where(m => m.deletedAt == null)
                .Where(m => m.chatId == id)
                .Select(m => new Message
                {
                    id = m.id,
                    chatId = m.chatId,
                    senderId = m.senderId,
                    content = m.content,
                    createdAt = m.createdAt
                }).ToListAsync();
        }

        public async Task<Message> UpdateMessage(Message message)
        {
            var existing = await _context.dbMessages.FindAsync(message.id);
            if (existing != null)
            {
                existing.senderId = message.senderId;
                existing.createdAt = message.createdAt;
                existing.content = message.content;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(Message message)
        {
            await _context.dbMessages.AddAsync(message);
            _context.SaveChanges();
        }

        public async Task DeleteMessage(Guid id)
        {
            var requirement = await _context.dbMessages.FindAsync(id);
            if (requirement != null)
            {
                requirement.deletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Message?> GetById(Guid id)
        {
            var response = await _context.dbMessages
                .Where(m => m.id == id)
                .Select(m => new Message 
                { 
                    id = m.id,
                    chatId = m.chatId,
                    content = m.content,
                    senderId = m.senderId,
                    createdAt = m.createdAt
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
    }
}
