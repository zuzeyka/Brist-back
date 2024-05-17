using Slush.Data.Entity.Profile;
<<<<<<< HEAD
using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameTopic : Category
    {
        public List<GamePosts> posts { get; set; }

        public GameTopic(string id,
                         string name,
                         string description,
                         string attachedId,
                         List<GamePosts> posts,
                         DateTime createdAt)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.attachedId = attachedId;
            this.posts = posts;
            this.createdAt = createdAt;
        }
=======


namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameTopic
    {
        public GameTopic()
        {
        }

        public GameTopic(Guid id, Guid attachedId, String? name, String? description, DateTime? createdAt)
        {
            this.id = id;
            this.attachedId = attachedId;
            this.name = name;
            this.description = description;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid attachedId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
