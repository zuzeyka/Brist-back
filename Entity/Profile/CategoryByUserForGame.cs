namespace Slush.Entity.Profile
{
    public class CategoryByUserForGame
    {
        public CategoryByUserForGame() { }

        public CategoryByUserForGame(Guid id, String name, String? image, DateTime createdAt) 
        {
            this.id = id;
            this.name = name;
            this.image = image;
            this.createdAt = createdAt;
        }

        public Guid id {  get; set; }
        public String name { get; set; } = null!;
        public String? image { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
