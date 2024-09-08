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

		public async Task<Game?> DeleteAsync(Guid id)
		{
			var existingGame = await _context.Games.FindAsync(id);

            if (existingGame != null)
            {
				_context.Games.Remove(existingGame);
				await _context.SaveChangesAsync();
				return existingGame;
            }

			return null;
        }

		public async Task<IEnumerable<Game>> GetAllAsync()
		{
			return await _context.Games
				.Include(x => x.Genres)
				.Include(x => x.Languages)
				.Include(x => x.Platforms)
				.ToListAsync();
		}

		public async Task<Game?> GetAsync(Guid id)
		{
			return await _context.Games
				.Include(x => x.Genres)
				.Include(x => x.Languages)
				.Include(x => x.Platforms)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Game?> UpdateAsync(Game game)
		{
			var existingGame = await _context.Games
				.Include(x => x.Genres)
				.Include(x => x.Languages)
				.Include(x => x.Platforms)
				.FirstOrDefaultAsync(x => x.Id == game.Id);

			if(existingGame != null)
			{
				existingGame.Name = game.Name;
				existingGame.Description = game.Description;
				existingGame.SystemRequirements = game.SystemRequirements;
				existingGame.Developer = game.Developer;
				existingGame.Price = game.Price;
				existingGame.YearOfRelease = game.YearOfRelease;
				existingGame.Genres = game.Genres;
				existingGame.Languages = game.Languages;
				existingGame.Platforms = game.Platforms;
				existingGame.ImageUrl = game.ImageUrl;

				await _context.SaveChangesAsync();
				return existingGame;
			}

			return null;
		}
	}
}
