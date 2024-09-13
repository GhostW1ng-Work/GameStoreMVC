using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<Language?> GetAsync(Guid id);
        Task<Language> AddAsync(Language language);
        Task<Language?> UpdateAsync(Language language);
        Task<Language?> DeleteAsync(Guid id);
        Task<int> GetCountAsync();
    }
}
