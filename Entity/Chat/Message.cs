namespace Slush.Entity.Chat
{
    public class Message
    {
        public Message() { }

        public Message(Guid id, Guid chatId, Guid senderId, String? content, DateTime createdAt)
        {
            this.id = id;
            this.chatId = chatId;
            this.senderId = senderId;
            this.content = content;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid chatId { get; set; }
        public Guid senderId { get; set; }
        public String? content { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
