using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IOwnedGameRepository
    {
        Task<List<OwnedGame>> GetAllOwnedGames();
        Task<OwnedGame> UpdateOwnedGame(OwnedGame ownedGame);
        Task Add(OwnedGame game);
        Task DeleteOwnedGame(Guid id);
        Task<OwnedGame?> GetById(Guid id);
        Task<List<OwnedGame?>> GetByUId(Guid id);
        Task<OwnedGame?> GetByGameId(Guid id, Guid uid);
        Task<List<OwnedGame?>> GetByIds(List<Guid> ids);
    }
}
