namespace Slush.Entity.Store.Product.Creators
{
    public class Publisher
    {
        public Publisher(string id, int subscribersCount, string name, string description, string avatar, string backgroundImage, string? urlForNewsPage)
        {
            this.id = id;
            this.subscribersCount = subscribersCount;
            this.name = name;
            this.description = description;
            this.avatar = avatar;
            this.backgroundImage = backgroundImage;
            this.urlForNewsPage = urlForNewsPage;
        }

        public String id { get; set; }
        public int subscribersCount { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String avatar { get; set; }
        public String backgroundImage { get; set; }
        public String? urlForNewsPage { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
