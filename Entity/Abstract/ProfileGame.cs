namespace Slush.Entity.Abstract
{
    public abstract class ProfileGame : DBRecord
    {
        public Guid ownedGameId { get; set; }
        public Guid userId { get; set; }
    }
}
