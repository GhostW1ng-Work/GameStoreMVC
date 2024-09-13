using Microsoft.AspNetCore.Identity;

namespace Web.GameStoreMVC.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<IdentityUser>> GetAllAsync(int pageNumber = 1, int pageSize = 3);
		Task<int> GetCountAsync();
	}
}
