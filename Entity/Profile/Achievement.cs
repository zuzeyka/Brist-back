namespace Slush.Entity.Profile
{
    public class Achievement
    {
        public Achievement() { }

        public Achievement(Guid id, String? urlForImage, String? description, int amountOfExperience, DateTime? createdAt) 
        {
            this.id = id;
            this.urlForImage = urlForImage;
            this.description = description;
            this.amountOfExperience = amountOfExperience;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public String? urlForImage { get; set; }
        public String? description { get; set; }
        public int amountOfExperience { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
