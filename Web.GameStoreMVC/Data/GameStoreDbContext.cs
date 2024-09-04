using Microsoft.EntityFrameworkCore;
using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Data
{
	public class GameStoreDbContext : DbContext
	{
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var decimalProps = modelBuilder.Model
			.GetEntityTypes()
			.SelectMany(t => t.GetProperties())
			.Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

			foreach (var property in decimalProps)
			{
				property.SetPrecision(18);
				property.SetScale(2);
			}
		}

		public DbSet<Game> Games { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Platform> Platforms { get; set; }
		public DbSet<Language> Languages { get; set; }
	}
}
