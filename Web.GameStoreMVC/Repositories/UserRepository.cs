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

        public async Task<IEnumerable<IdentityUser>> GetAllAsync()
		{
			var users = await _context.Users.ToListAsync();

			var superAdminUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == "SuperAdmin");

			if(superAdminUser != null)
			{
				users.Remove(superAdminUser);
			}

			return users;
		}
	}
}
