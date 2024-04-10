using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class GameByRequirements : DBRecord
    {
        public String recomendedRequirementsId { get; set; }
        public String minimalRequirementsId { get; set; }
    }
}
