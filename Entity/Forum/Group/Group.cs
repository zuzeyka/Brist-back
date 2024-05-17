using Slush.Data.Entity.Profile;
<<<<<<< HEAD
using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community
{
    public class Group : Category
    {
        public List<User> users { get; set; }
        public List<Topic> topics { get; set; }
        public List<GroupComment> comments { get; set; }


        public Group(String id,
                     String name,
                     String description,
                     List<User> users,
                     List<Topic> topics,
                     List<GroupComment> comments,
                     DateTime createdAt,
                     DateTime? deleteAt)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.users = users;
            this.topics = topics;
            this.comments = comments;
            this.createdAt = createdAt;
            this.deleteAt = deleteAt;
        }
=======


namespace Slush.Data.Entity.Community
{
    public class Group
    {
        public Group()
        {
        }

        public Group(Guid id, Guid attachedId, String? name, String? description, DateTime? createdAt)
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
