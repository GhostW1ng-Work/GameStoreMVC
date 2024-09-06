using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
	public interface IGenreRepository
	{
		Task<IEnumerable<Genre>> GetAllAsync();
		Task<Genre?> GetAsync(Guid id);
		Task<Genre> AddAsync(Genre genre);
		Task<Genre?> UpdateAsync(Genre genre);
		Task<Genre?> DeleteAsync(Guid id);
		Task<int> GetCountAsync();
	}
}
