using Slush.Data.Entity;

namespace Slush.Repositories.IRepository
{
    public interface ILanguageInGameRepository
    {
        Task<List<LanguageInGame>> GetAllLanguageInGames();
        Task<LanguageInGame> UpdateLanguageInGame(LanguageInGame language);
        Task Add(LanguageInGame language);
        Task DeleteLanguageInGame(Guid id);
        Task<LanguageInGame?> GetById(Guid id);
        Task<List<LanguageInGame?>> GetByIds(List<Guid> id);
    }
}
