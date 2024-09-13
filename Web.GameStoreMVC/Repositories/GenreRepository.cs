using Microsoft.EntityFrameworkCore;
using Web.GameStoreMVC.Data;
using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
	public class GenreRepository : IGenreRepository
	{
		private readonly GameStoreDbContext _context;

        public GenreRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> AddAsync(Genre genre)
		{
			await _context.Genres.AddAsync(genre);
			await _context.SaveChangesAsync();
			return genre;
		}

		public async Task<Genre?> DeleteAsync(Guid id)
		{
			var existingGenre = await _context.Genres.FindAsync(id);

			if (existingGenre != null)
			{
				_context.Genres.Remove(existingGenre);
				await _context.SaveChangesAsync();
				return existingGenre;
			}

			return null;
		}

		public async Task<IEnumerable<Genre>> GetAllAsync(int pageNumber, int pageSize)
		{
			var query = _context.Genres.AsQueryable();
			var skipResults = (pageNumber - 1) * pageSize;
			query = query.Skip(skipResults).Take(pageSize);
			return await query.ToListAsync();
		}

		public async Task<Genre?> GetAsync(Guid id)
		{
			return await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<int> GetCountAsync()
		{
			return await _context.Genres.CountAsync();
		}

		public async Task<Genre?> UpdateAsync(Genre genre)
		{
			var existingGenre = await _context.Genres.FindAsync(genre.Id);

			if (existingGenre != null)
			{
				existingGenre.Name = genre.Name;
				
				await _context.SaveChangesAsync();
				return existingGenre;
			}

			return null;
		}
	}
}
