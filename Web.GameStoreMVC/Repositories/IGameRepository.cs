using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
	public interface IGameRepository
	{
		Task<IEnumerable<Game>> GetAllAsync();
		Task<Game?> GetAsync(Guid id);
		Task<Game> AddAsync(Game game);
		Task<Game?> UpdateAsync(Game game);
		Task<Game?> DeleteAsync(Guid id);
	}
}
