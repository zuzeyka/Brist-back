using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class AuthorByPost : DBRecord
    {
        public Guid authorId {  get; set; }
        public String postId { get; set; }
    }
}
