using Microsoft.EntityFrameworkCore;
using Web.GameStoreMVC.Data;
using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
	public class PlatformRepository : IPlatformRepository
	{
		private readonly GameStoreDbContext _context;

        public PlatformRepository(GameStoreDbContext context)
        {
			_context = context;   
        }

        public async Task<Platform> AddAsync(Platform platform)
		{
			await _context.Platforms.AddAsync(platform);
			await _context.SaveChangesAsync();
			return platform;
		}

		public async Task<Platform?> DeleteAsync(Guid id)
		{
			var existingPlatform = await _context.Platforms.FindAsync(id);

			if(existingPlatform != null)
			{
				_context.Platforms.Remove(existingPlatform);
				await _context.SaveChangesAsync();
				return existingPlatform;
			}
			
			return null;
		}

		public async Task<IEnumerable<Platform>> GetAllAsync(int pageNumber = 1, int pageSize = 3)
		{
			var query = _context.Platforms.AsQueryable();
			var skipResults = (pageNumber - 1) * pageSize;
			query = query.Skip(skipResults).Take(pageSize);
			return await query.ToListAsync();
		}

		public async Task<Platform?> GetAsync(Guid id)
		{
			return await _context.Platforms.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<int> GetCountAsync()
		{
			return await _context.Platforms.CountAsync();
		}

		public async Task<Platform?> UpdateAsync(Platform platform)
		{
			var existingPlatform = await _context.Platforms.FirstOrDefaultAsync(x => x.Id == platform.Id);

			if (existingPlatform != null)
			{
				existingPlatform.Name = platform.Name;

				await _context.SaveChangesAsync();
				return existingPlatform;
			}

			return null;
		}
	}
}
