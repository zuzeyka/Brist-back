namespace Slush.Data.Entity
{
    public class MaximumSystemRequirement
    {
        public MaximumSystemRequirement()
        {
        }

        public MaximumSystemRequirement(Guid id, Guid gameId, String? oS, String? processor, String? rAM, String? video, String? freeDiskSpace, DateTime? createdAt)
        {
            this.id = id;
            this.gameId = gameId;
            this.OS = oS;
            this.processor = processor;
            this.RAM = rAM;
            this.video = video;
            this.freeDiskSpace = freeDiskSpace;
            this.createdAt = createdAt;
        }



        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public String? OS { get; set; }
        public String? processor { get; set; }
        public String? RAM { get; set; }
        public String? video { get; set; }
        public String? freeDiskSpace { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
