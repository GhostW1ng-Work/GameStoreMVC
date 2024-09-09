using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.GameStoreMVC.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			const string USER_ID = "8fdf8098-12d2-4580-9090-a50731e1bd14";
			const string ADMIN_ID = "834e063b-27ac-455b-a9cf-37f19e375266";
			const string SUPER_ADMIN_ID = "2e04e947-c633-41e6-b33c-1c82ece8c96b";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name = "User",
					NormalizedName = "User",
					Id = USER_ID,
					ConcurrencyStamp = USER_ID
				},

				new IdentityRole
				{
					Name = "Admin",
					NormalizedName = "Admin",
					Id = ADMIN_ID,
					ConcurrencyStamp = ADMIN_ID
				},

				new IdentityRole
				{
					Name = "SuperAdmin",
					NormalizedName = "SuperAdmin",
					Id = SUPER_ADMIN_ID,
					ConcurrencyStamp = SUPER_ADMIN_ID
				},
			};

			builder.Entity<IdentityRole>().HasData(roles);

			var superAdminUserId = "f461d4cb-b313-46b9-8005-cd3975a1f735";

			var superAdminUser = new IdentityUser
			{
				UserName = "SuperAdmin",
				Email = "superadmingamestore@gmail.com",
				NormalizedEmail = "superadmingamestore@gmail.com".ToUpper(),
				NormalizedUserName = "SuperAdmin".ToUpper(),
				Id = superAdminUserId
			};

			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
				.HashPassword(superAdminUser, "SuperAdmin323");

			builder.Entity<IdentityUser>().HasData(superAdminUser);

			var superAdminRole = new List<IdentityUserRole<string>>()
			{
				new IdentityUserRole<string>
				{
					RoleId = USER_ID,
					UserId=superAdminUserId
				},

				new IdentityUserRole<string>
				{
					RoleId = ADMIN_ID,
					UserId=superAdminUserId
				},

				new IdentityUserRole<string>
				{
					RoleId = SUPER_ADMIN_ID,
					UserId=superAdminUserId
				},
			};

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRole);
		}
	}
}
