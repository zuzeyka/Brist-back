namespace FullStackBrist.Server.Models.Requirements
{
    public class MinimalSystemRequirementModel
    {
        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public String? OS { get; set; }
        public String? processor { get; set; }
        public String? RAM { get; set; }
        public String? video { get; set; }
        public String? freeDiskSpace { get; set; }
        public DateTime createdAt { get; set; }
    }
}
