using Slush.Data.Entity.Profile;
<<<<<<< HEAD
using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community
{
    public class GroupComment : DBRecord
    {
        public String groupId { get; set; }
        public String content { get; set; }


        public GroupComment(String id,
                            String content,
                            DateTime createdAt,
                            String userId,
                            String groupId)
        {
            this.id = id;
            this.content = content;
            this.createdAt = createdAt;
            this.groupId = groupId;
        }
=======


namespace Slush.Data.Entity.Community
{
    public class GroupComment
    {
        public GroupComment()
        {
        }

        public GroupComment(Guid id, Guid groupId, String? content, Guid userId, DateTime? createdAt)
        {
            this.id = id;
            this.groupId = groupId;
            this.content = content;
            this.userId = userId;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public Guid groupId { get; set; }
        public String? content { get; set; }
        public Guid userId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
