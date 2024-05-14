namespace FullStackBrist.Server.Models.Categories
{
    public class CategoryByAuthorModel
    {
        public Guid id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String image { get; set; }
        public DateTime createdAt { get; set; }

    }
}
