namespace Slush.Models.Profile
{
    public class CategoryByUserForGameModel
    {
        public Guid id { get; set; }
        public String name { get; set; } = null!;
        public String? image {  get; set; }
        public DateTime createdAt {  get; set; }
    }
}
