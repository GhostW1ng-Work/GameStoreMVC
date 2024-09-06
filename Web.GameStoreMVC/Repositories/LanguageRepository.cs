using Microsoft.EntityFrameworkCore;
using Web.GameStoreMVC.Data;
using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly GameStoreDbContext _context;

        public LanguageRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Language> AddAsync(Language language)
        {
            await _context.AddAsync(language);
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task<Language?> DeleteAsync(Guid id)
        {
            var existingLanguage = await _context.Languages.FindAsync(id);

            if (existingLanguage != null)
            {
                _context.Languages.Remove(existingLanguage);
                await _context.SaveChangesAsync();
                return existingLanguage;
            }

            return null;
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync(); 
        }

        public async Task<Language?> GetAsync(Guid id)
        {
            return await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Languages.CountAsync();
        }

        public async Task<Language?> UpdateAsync(Language language)
        {
            var existingLanguage =  await _context.Languages.FirstOrDefaultAsync(x => x.Id == language.Id);

            if (existingLanguage != null)
            {
                existingLanguage.Name = language.Name;

                await _context.SaveChangesAsync();
                return existingLanguage;
            }

            return null;
        }
    }
}
