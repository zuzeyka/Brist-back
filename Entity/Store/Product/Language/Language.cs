

namespace Slush.Data.Entity
{
    public class Language
    {
        public Language()
        {
        }

        public Language(Guid id, String? name, DateTime? createdAt)
        {
            this.id = id;
            this.name = name;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String? name { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
