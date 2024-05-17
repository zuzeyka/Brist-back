namespace Slush.Entity.Chat
{
    public class Chat
    {
        public Chat()
        {
        }

        public Chat(Guid id, Guid firstUser, Guid secondUser, DateTime createdAt)
        {
            this.id = id;
            this.firstUser = firstUser;
            this.secondUser = secondUser;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid firstUser { get; set; }
        public Guid secondUser { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
