namespace FullStackBrist.Server.Models.Requirements
{
    public class SystemRequirementModel
    {
        public Guid id { get; set; }
        public String gameId { get; set; }
        public String OS { get; set; }
        public String processor { get; set; }
        public String RAM { get; set; }
        public String video { get; set; }
        public String freeDiskSpace { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
