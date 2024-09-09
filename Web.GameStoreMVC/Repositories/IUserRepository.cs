using Microsoft.AspNetCore.Identity;

namespace Web.GameStoreMVC.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<IdentityUser>> GetAllAsync();
	}
}
