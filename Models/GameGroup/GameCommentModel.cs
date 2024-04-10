namespace FullStackBrist.Server.Models.GameGroup
{
    public class GameCommentModel
    {
        public Guid id { get; set; }
        public String gamePostId { get; set; }
        public String content { get; set; }
    }
}
