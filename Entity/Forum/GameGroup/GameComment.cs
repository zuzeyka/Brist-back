using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameComment
    {
        public GameComment(string id, string gamePostId, string content)
        {
            this.id = id;
            this.gamePostId = gamePostId;
            this.content = content;
        }

        public String id { get; set; }
        public String gamePostId { get; set; }
        public String content { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
        public virtual Author Author { get; set; } = null!;
    }
}
