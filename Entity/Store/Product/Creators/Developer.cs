namespace Slush.Entity.Store.Product.Creators
{
    public class Developer
    {
        public Developer()
        {
        }

        public Developer(Guid id, int subscribersCount, String? name, String? description, String? avatar, String? backgroundImage, String? urlForNewsPage, DateTime? createdAt)
        {
            this.id = id;
            this.subscribersCount = subscribersCount;
            this.name = name;
            this.description = description;
            this.avatar = avatar;
            this.backgroundImage = backgroundImage;
            this.urlForNewsPage = urlForNewsPage;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public int subscribersCount { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public String? avatar { get; set; }
        public String? backgroundImage { get; set; }
        public String? urlForNewsPage { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}