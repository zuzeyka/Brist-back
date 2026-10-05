namespace Slush.Data.Entity
{
    public class Categories
    {
        public Categories()
        {
        }

        public Categories(Guid id, String? name, String? description, DateTime? createdAt, String? kind = "genre")
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.createdAt = createdAt;
            this.kind = kind;
        }



        public Guid id { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        // "genre" (default), "platform", "type" (player count) or "feature" —
        // lets the same tag/join-table shape back multiple independent filter
        // groups instead of a new table per group.
        public String? kind { get; set; } = "genre";

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }

}
    
