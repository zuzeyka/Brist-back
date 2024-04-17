using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class PostByAythor : DBRecord
    {
        public String postId { get; set; }
        public Guid authorId { get; set; }
    }
}
