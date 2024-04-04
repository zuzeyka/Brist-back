using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class Language
    {
        public Language(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public String id { get; set; }
        public String name { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
