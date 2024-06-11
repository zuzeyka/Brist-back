namespace Slush.Models.Chat
{
    public class MessageModel
    {
        public Guid chatId { get; set; }
        public Guid senderId { get; set; }
        public String? content { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
