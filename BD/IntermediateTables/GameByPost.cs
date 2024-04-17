using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class GameByPost : DBRecord
    {
        public Guid gameId {  get; set; }
        public String postId { get; set; }
    }
}
