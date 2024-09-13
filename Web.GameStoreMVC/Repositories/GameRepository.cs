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

		public async Task<IEnumerable<Game>> GetAllAsync(
			string? searchQuery = null,
			string? sortBy = null,
			string? sortDirection = null,
			int pageSize = 3,
			int pageNumber = 1)
		{
			var query = _context.Games.AsQueryable();

			if(!string.IsNullOrWhiteSpace(searchQuery))
			{
				query = query.Where(x => x.Name.Contains(searchQuery)
				|| x.Developer.Contains(searchQuery));
			}

			if(!string.IsNullOrWhiteSpace(sortBy))
			{
				var isDesc = string.Equals(sortDirection,"Desc", StringComparison.OrdinalIgnoreCase);

				if(string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
				{
					query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
				}

				if (string.Equals(sortBy, "ShortDescription", StringComparison.OrdinalIgnoreCase))
				{
					query = isDesc ? query.OrderByDescending(x => x.ShortDescription)
						: query.OrderBy(x => x.ShortDescription);
				}

				if (string.Equals(sortBy, "YearOfRelease", StringComparison.OrdinalIgnoreCase))
				{
					query = isDesc ? query.OrderByDescending(x => x.YearOfRelease)
						: query.OrderBy(x => x.YearOfRelease);
				}

				if (string.Equals(sortBy, "Price", StringComparison.OrdinalIgnoreCase))
				{
					query = isDesc ? query.OrderByDescending(x => x.Price)
						: query.OrderBy(x => x.Price);
				}
			}

			var skipResults = (pageNumber - 1) * pageSize;
			query = query.Skip(skipResults).Take(pageSize);

			return await query
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

		public async Task<int> GetCountAsync()
		{
			return await _context.Games.CountAsync(); 
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
				existingGame.ShortDescription = game.ShortDescription;
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
