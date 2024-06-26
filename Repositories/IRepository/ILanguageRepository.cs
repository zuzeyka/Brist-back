using Slush.Data.Entity;

namespace Slush.Repositories.IRepository
{
    public interface ILanguageRepository
    {
        Task<List<Language>> GetAllLanguages();
        Task<Language> UpdateLanguage(Language language);
        Task Add(Language language);
        Task DeleteLanguage(Guid id);
        Task<Language?> GetById(Guid id);
        Task<List<Language?>> GetByIds(List<Guid> id);
    }
}
