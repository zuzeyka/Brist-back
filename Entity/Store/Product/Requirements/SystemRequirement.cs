<<<<<<< HEAD
﻿using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class SystemRequirement : DBRecord
    {
        protected String gameId { get; set; }
        protected String OS { get; set; }
        protected String processor { get; set; }
        protected String RAM { get; set; }
        protected String video { get; set; }
        protected String freeDiskSpace { get; set; }
=======
﻿

namespace Slush.Data.Entity
{
    public class SystemRequirement
    {
        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public String? OS { get; set; }
        public String? processor { get; set; }
        public String? RAM { get; set; }
        public String? video { get; set; }
        public String? freeDiskSpace { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
