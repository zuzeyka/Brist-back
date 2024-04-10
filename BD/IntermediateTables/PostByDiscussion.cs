using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class PostByDiscussion : DBRecord
    {
        public String postId { get; set; }
        public String discussionId { get; set; }
    }
}
