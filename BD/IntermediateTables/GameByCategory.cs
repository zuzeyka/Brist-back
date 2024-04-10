using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class GameByCategory : DBRecord
    {
        public String gameId { get; set; }
        public String categoryId { get; set; }
    }
}
