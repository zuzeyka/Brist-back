namespace Slush.Entity.Store.Product
{
    public class GameEvent
    {
        public GameEvent()
        {
        }

        public GameEvent(Guid id, String? name, String? description, DateTime startAt, DateTime endAt, DateTime? createdAt)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.startAt = startAt;
            this.endAt = endAt;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
