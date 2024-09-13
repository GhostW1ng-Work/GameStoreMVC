using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
	public interface IGameRepository
	{
		Task<IEnumerable<Game>> GetAllAsync(
			string? searchQuery = null,
			string? sortBy = null,
			string? sortDirection = null,
			int pageSize = 100,
			int pageNumber = 1);

		Task<Game?> GetAsync(Guid id);
		Task<Game> AddAsync(Game game);
		Task<Game?> UpdateAsync(Game game);
		Task<Game?> DeleteAsync(Guid id);
		Task<int> GetCountAsync();
	}
}
