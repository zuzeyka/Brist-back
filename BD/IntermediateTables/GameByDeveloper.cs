using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class GameByDeveloper : DBRecord
    {
        public Guid gameId { get; set; }
        public Guid developerId { get; set; }
        public Guid publisherId { get; set; }
    }
}
