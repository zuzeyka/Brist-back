using Slush.Data.Entity.Profile;
<<<<<<< HEAD
using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community
{
    public class Topic : Category
    {
        public String attachedId { get; set; }

        public User author { get; set; }
        public List<Post> posts { get; set; }

        public Topic(String id,
                     String name,
                     String description,
                     String attachedId,
                     DateTime createdAt,
                     User author,
                     List<Post> posts)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.attachedId = attachedId;
            this.createdAt = createdAt;
            this.author = author;
            this.posts = posts;
        }
=======


namespace Slush.Data.Entity.Community
{
    public class Topic
    {
        public Topic()
        {
        }

        public Topic(Guid id, Guid attachedId, String? name, String? description, Guid authorId, Guid postId, DateTime? createdAt)
        {
            this.id = id;
            this.attachedId = attachedId;
            this.name = name;
            this.description = description;
            this.authorId = authorId;
            this.postId = postId;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public Guid attachedId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public Guid authorId { get; set; }
        public Guid postId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }

>>>>>>> development_branch
    }
}
