using Slush.Data.Entity.Profile;
using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community
{
    public class Group
    {
        public Group()
        {
        }

        public Group(Guid id, Guid attachedId, String name, String description, DateTime? createdAt)
        {
            this.id = id;
            this.attachedId = attachedId;
            this.name = name;
            this.description = description;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid attachedId { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
        public virtual List<User> users { get; set; }               = null!;
        public virtual List<Topic> topics { get; set; }             = null!;
        public virtual List<GroupComment> comments { get; set; } = null!;
    }
}
