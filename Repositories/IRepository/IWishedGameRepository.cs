using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IWishedGameRepository
    {
        Task<List<WishedGame>> GetAllWishedGames();
        Task<WishedGame> UpdateWishedGame(WishedGame wishedGame);
        Task Add(WishedGame game);
        Task DeleteWishedGame(Guid id);
        Task<WishedGame?> GetById(Guid id);
        Task<WishedGame?> GetByUserAndGameId(Guid id, Guid gameId);
        Task<List<WishedGame?>> GetIds(List<Guid> ids);
    }
}
