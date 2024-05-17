<<<<<<< HEAD
﻿using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class Language : DBRecord
    {
        public String name { get; set; }
        public Language(String id,
                        String name,
                        DateTime createdAt)
=======
﻿

namespace Slush.Data.Entity
{
    public class Language
    {
        public Language()
        {
        }

        public Language(Guid id, String? name, DateTime? createdAt)
>>>>>>> development_branch
        {
            this.id = id;
            this.name = name;
            this.createdAt = createdAt;
        }
<<<<<<< HEAD
=======

        public Guid id { get; set; }
        public String? name { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
