using Microsoft.AspNetCore.Mvc;
using Web.GameStoreMVC.Models.Domain;
using Web.GameStoreMVC.Models.ViewModels;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
	public class AdminGenreController : Controller
	{
		private readonly IGenreRepository _genreRepository;

		public AdminGenreController(IGenreRepository genreRepository)
		{
			_genreRepository = genreRepository;
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddGenreRequest addGenreRequest)
		{
			if (ModelState.IsValid)
			{
				var genre = new Genre
				{
					Name = addGenreRequest.Name
				};

				await _genreRepository.AddAsync(genre);

				return RedirectToAction(nameof(List));
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var genres = await _genreRepository.GetAllAsync();
			return View(genres);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var genre = await _genreRepository.GetAsync(id);

			if (genre != null)
			{
				var editGenreRequest = new EditGenreRequest
				{
					Id = genre.Id,
					Name = genre.Name
				};
				return View(editGenreRequest);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditGenreRequest editGenreRequest)
		{
			var genre = new Genre
			{
				Id = editGenreRequest.Id,
				Name = editGenreRequest.Name
			};
			var updatedGenre = await _genreRepository.UpdateAsync(genre);

			return RedirectToAction(nameof(List), new { id = editGenreRequest.Id });
		}

		[HttpPost]
		public async Task<IActionResult> Delete(EditGenreRequest editGenreRequest)
		{
			var deletedGenre = await _genreRepository.DeleteAsync(editGenreRequest.Id);

			if(deletedGenre != null)
			{
				return RedirectToAction(nameof(List));
			}

			return RedirectToAction(nameof(List), new {id = editGenreRequest.Id});
		}
	}
}
