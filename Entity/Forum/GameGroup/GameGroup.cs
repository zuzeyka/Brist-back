using Slush.Data.Entity.Profile;
<<<<<<< HEAD
using Slush.Entity.Abstract;
=======

>>>>>>> development_branch
using Slush.Entity.Store.Product;

namespace Slush.Data.Entity.Community.GameGroup
{
<<<<<<< HEAD
    public class GameGroup : DBRecord
    {
        public GameInShop game { get; set; }
        public List<GameNews> news { get; set; }
        public List<Video> videos { get; set; }
        public List<Screenshot> screenshots { get; set; }
        public List<GameTopic> topics { get; set; }

        public GameGroup(String id,
                         GameInShop game,
                         List<GameNews> news,
                         List<GameVideo> videos,
                         List<GameScreenshot> screenshots,
                         List<GameTopic> topics,
                         DateTime createdAt)
        {
            this.id = id;
            this.game = game;
            this.news = news;
            this.videos = videos;
            this.screenshots = screenshots;
            this.topics = topics;
            this.createdAt = createdAt;
        }

=======
    public class GameGroup
    {
        public GameGroup()
        {
        }

        public GameGroup(Guid id, Guid gameId, DateTime? createdAt)
        {
            this.id = id;
            this.gameId = gameId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
