namespace Slush.Models.Chat
{
    public class ChatModel
    {
        public Guid firstUser { get; set; }
        public Guid secondUser { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
