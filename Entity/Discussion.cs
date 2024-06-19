namespace Slush.Entity
{
    public class Discussion
    {
        public Discussion() { }

        public Discussion(Guid id, Guid authorId, Guid attachedId, String? content, int likesCount, int rate, DateTime? createdAt) 
        {
            this.id = id;
            this.authorId = authorId;
            this.content = content;
            this.likesCount = likesCount;
            this.rate = rate;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid authorId { get; set; }
        public Guid attachedId { get; set; }
        public String? content { get; set; }
        public int likesCount {  get; set; }
        public int rate {  get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set;}
    }
}
