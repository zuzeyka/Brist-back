namespace FullStackBrist.Server.Models.ShopContent
{
    public class GameEventModel
    {
        public String? name { get; set; }
        public String? description { get; set; }
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
