namespace FullStackBrist.Server.Models.Creators
{
    public class DeveloperModel
    {
        public Guid id { get; set; }  
        public String name { get; set; }
        public int subscribersCount { get; set; }
        public String? urlForNewsPage { get; set; }
        public String? description { get; set; } 
        public String? avatar {  get; set; }
        public String? backgroundImage { get; set; }
        public DateTime createdAt { get; set; }
    }
}
