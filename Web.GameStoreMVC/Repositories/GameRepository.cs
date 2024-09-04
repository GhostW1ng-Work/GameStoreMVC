using Microsoft.EntityFrameworkCore;
using Web.GameStoreMVC.Data;
using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
	public class GameRepository : IGameRepository
	{
		private readonly GameStoreDbContext _context;

        public GameRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Game> AddAsync(Game game)
		{
			await _context.Games.AddAsync(game);
			await _context.SaveChangesAsync();
			return game;
		}

		public Task<Game?> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Game>> GetAllAsync()
		{
			return await _context.Games.ToListAsync();
		}

		public async Task<Game?> GetAsync(Guid id)
		{
			return await _context.Games.FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task<Game?> UpdateAsync(Game game)
		{
			throw new NotImplementedException();
		}
	}
}
