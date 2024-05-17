namespace Slush.Entity.Store.Product.Creators
{
<<<<<<< HEAD
    public class Publisher : Abstract.Profile
    {
        public String? urlForNewsPage { get; set; }
        public Publisher(String id,
                         String description,
                         String avatar,
                         String backgroundImage,
                         String name,
                         String? urlForNewsPage,
                         DateTime createdAt,
                         List<object> showcases,
                         DateTime? deleteAt)
        {
            this.id = id;
            this.name = name;
            this.avatar = avatar;
            this.backgroundImage = backgroundImage;
            this.description = description;
            this.urlForNewsPage = urlForNewsPage;
            this.showcases = showcases;
            this.createdAt = createdAt;
            this.deleteAt = deleteAt;
        }
=======
    public class Publisher
    {
        public Publisher() { }
        public Publisher(Guid id, int subscribersCount, String? name, String? description, String? avatar, String? backgroundImage, String? urlForNewsPage, DateTime? createdAt)
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
>>>>>>> development_branch
    }
}
