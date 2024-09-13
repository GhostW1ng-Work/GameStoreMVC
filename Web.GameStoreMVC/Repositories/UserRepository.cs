using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.GameStoreMVC.Data;

namespace Web.GameStoreMVC.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AuthDbContext _context;

		public UserRepository(AuthDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<IdentityUser>> GetAllAsync(int pageNumber = 1, int pageSize = 3)
		{
			var query = _context.Users.AsQueryable();
			var skipResults = (pageNumber - 1) * pageSize;
			query = query.Skip(skipResults).Take(pageSize);

			var users = await query.ToListAsync();

			var superAdminUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == "SuperAdmin");

			if (superAdminUser != null)
			{
				users.Remove(superAdminUser);
			}

			return users;
		}

		public async Task<int> GetCountAsync()
		{
			return await _context.Users.CountAsync();
		}
	}
}
