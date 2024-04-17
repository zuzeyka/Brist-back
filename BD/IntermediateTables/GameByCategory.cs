using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class GameByCategory : DBRecord
    {
        public Guid gameId { get; set; }
        public Guid categoryId { get; set; }
    }
}
