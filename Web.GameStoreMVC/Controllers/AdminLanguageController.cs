using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.GameStoreMVC.Models.Domain;
using Web.GameStoreMVC.Models.ViewModels;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminLanguageController : Controller
    {
        private readonly ILanguageRepository _languageRepository;

        public AdminLanguageController(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var languages = await _languageRepository.GetAllAsync();

            return View(languages);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLanguageRequest addLanguageRequest)
        {
            if(ModelState.IsValid)
            {
                var language = new Language
                {
                    Name = addLanguageRequest.Name
                };

                await _languageRepository.AddAsync(language);
                return RedirectToAction(nameof(List));
            }

            return View();
        }

        [HttpGet] 
        public async Task<IActionResult> Edit(Guid id)
        {
            var language = await _languageRepository.GetAsync(id);

            if(language != null) 
            {
                var updatedLanguage = new EditLanguageRequest
                {
                    Id = language.Id,
                    Name = language.Name
                };
                return View(updatedLanguage);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditLanguageRequest editLanguageRequest)
        {
            var language = new Language
            {
                Id = editLanguageRequest.Id,
                Name = editLanguageRequest.Name
            };
            await _languageRepository.UpdateAsync(language);

            return RedirectToAction(nameof(List), new {id = editLanguageRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditGenreRequest editGenreRequest)
        {
            var deletedGenre = await _languageRepository.DeleteAsync(editGenreRequest.Id);

            if(deletedGenre != null)
            {
                return RedirectToAction(nameof(List));
            }

            return RedirectToAction(nameof(List), new {id = editGenreRequest.Id});
        }
    }
}
