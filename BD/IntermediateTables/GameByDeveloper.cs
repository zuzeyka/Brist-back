using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class GameByDeveloper : DBRecord
    {
        public String gameId { get; set; }
        public String developerId { get; set; }
        public String publisherId { get; set; }
    }
}
