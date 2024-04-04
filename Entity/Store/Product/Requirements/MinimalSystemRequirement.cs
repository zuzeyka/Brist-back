namespace Slush.Data.Entity
{
    public class MinimalSystemRequirement
    {
        public MinimalSystemRequirement(string id, string gameId, string oS, string processor, string rAM, string video, string freeDiskSpace)
        {
            this.id = id;
            this.gameId = gameId;
            OS = oS;
            this.processor = processor;
            RAM = rAM;
            this.video = video;
            this.freeDiskSpace = freeDiskSpace;
        }

        public String id { get; set; }
        public String gameId { get; set; }
        public String OS { get; set; }
        public String processor { get; set; }
        public String RAM { get; set; }
        public String video { get; set; }
        public String freeDiskSpace { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
