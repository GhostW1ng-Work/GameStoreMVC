using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.GameStoreMVC.Models.Domain;
using Web.GameStoreMVC.Models.ViewModels;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
	public class AdminGamesController : Controller
	{
		private readonly IGameRepository _gameRepository;
		private readonly IGenreRepository _genreRepository;
		private readonly ILanguageRepository _languageRepository;
		private readonly IPlatformRepository _platformRepository;

		public AdminGamesController(IGameRepository gameRepository,
			IGenreRepository genreRepository,
			ILanguageRepository languageRepository,
			IPlatformRepository platformRepository)
		{
			_gameRepository = gameRepository;
			_genreRepository = genreRepository;
			_languageRepository = languageRepository;
			_platformRepository = platformRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var genres = await _genreRepository.GetAllAsync();
			var languages = await _languageRepository.GetAllAsync();
			var platforms = await _platformRepository.GetAllAsync();

			var viewModel = new AdminGameAddRequest
			{
				Genres = genres.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
				Languages = languages.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
				Platforms = platforms.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AdminGameAddRequest adminGameAddRequest)
		{
			var game = new Game
			{
				Name = adminGameAddRequest.Name,
				Description = adminGameAddRequest.Description,
				SystemRequirements = adminGameAddRequest.SystemRequirements,
				Developer = adminGameAddRequest.Developer,
				Price = adminGameAddRequest.Price,
				YearOfRelease = adminGameAddRequest.YearOfRelease
			};

			var selectedGenres = new List<Genre>();
			var selectedLanguages = new List<Language>();
			var selectedPlatforms = new List<Platform>();

			foreach (var genre in adminGameAddRequest.SelectedGenres)
			{
				var selectedGenre = Guid.Parse(genre);
				var existingGenre = await _genreRepository.GetAsync(selectedGenre);

				if (existingGenre != null)
				{
					selectedGenres.Add(existingGenre);
				}
			}

			foreach (var language in adminGameAddRequest.SelectedLanguages)
			{
				var selectedlanguage = Guid.Parse(language);
				var existinglanguage = await _languageRepository.GetAsync(selectedlanguage);

				if (existinglanguage != null)
				{
					selectedLanguages.Add(existinglanguage);
				}
			}

			foreach (var platform in adminGameAddRequest.SelectedPlatforms)
			{
				var selectedPlatform = Guid.Parse(platform);
				var existingPlatform = await _platformRepository.GetAsync(selectedPlatform);

				if (existingPlatform != null)
				{
					selectedPlatforms.Add(existingPlatform);
				}
			}

			game.Genres = selectedGenres;
			game.Languages = selectedLanguages;
			game.Platforms = selectedPlatforms;

			await _gameRepository.AddAsync(game);
			return RedirectToAction(nameof(Add));
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var games = await _gameRepository.GetAllAsync();
			return View(games);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var game = await _gameRepository.GetAsync(id);
			var genres = await _genreRepository.GetAllAsync();
			var languages = await _languageRepository.GetAllAsync();
			var platforms = await _platformRepository.GetAllAsync();

			if (game != null)
			{
				var editedGame = new AdminEditGameRequest
				{
					Id = game.Id,
					Name = game.Name,
					Description = game.Description,
					SystemRequirements = game.SystemRequirements,
					Developer = game.Developer,
					Price = game.Price,
					YearOfRelease = game.YearOfRelease,
					Genres = genres.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}),
					SelectedGenres = game.Genres.Select(x => x.Id.ToString()).ToArray(),


					Languages = languages.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}),
					SelectedLanguages = game.Languages.Select(x => x.Id.ToString()).ToArray(),


					Platforms = platforms.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}),
					SelectedPlatforms = game.Platforms.Select(x => x.Id.ToString()).ToArray()
				};
				return View(editedGame);
			}
			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(AdminEditGameRequest adminEditGameRequest)
		{
			var game = new Game
			{
				Id = adminEditGameRequest.Id,
				Name = adminEditGameRequest.Name,
				Description = adminEditGameRequest.Description,
				SystemRequirements = adminEditGameRequest.SystemRequirements,
				Developer = adminEditGameRequest.Developer,
				Price = adminEditGameRequest.Price,
				YearOfRelease = adminEditGameRequest.YearOfRelease
			};

			var editedGenres = new List<Genre>();
			var editedLanguages = new List<Language>();
			var editedPlatforms = new List<Platform>();

			foreach (var selectedGenre in adminEditGameRequest.SelectedGenres)
			{
				if (Guid.TryParse(selectedGenre, out var genreId))
				{
					var foundGenre = await _genreRepository.GetAsync(genreId);

					if (foundGenre != null)
					{
						editedGenres.Add(foundGenre);
					}
				}
			}

			foreach (var selectedLanguage in adminEditGameRequest.SelectedLanguages)
			{
				if (Guid.TryParse(selectedLanguage, out var languageId))
				{
					var foundLanguage = await _languageRepository.GetAsync(languageId);

					if (foundLanguage != null)
					{
						editedLanguages.Add(foundLanguage);
					}
				}
			}

			foreach (var selectedPlatform in adminEditGameRequest.SelectedPlatforms)
			{
				if (Guid.TryParse(selectedPlatform, out var platformId))
				{
					var foundPlatform = await _platformRepository.GetAsync(platformId);

					if (foundPlatform != null)
					{
						editedPlatforms.Add(foundPlatform);
					}
				}
			}

			game.Genres = editedGenres;
			game.Languages = editedLanguages;
			game.Platforms = editedPlatforms;

			var updatedGame = await _gameRepository.UpdateAsync(game);

			if (updatedGame != null)
			{
				return RedirectToAction(nameof(List));
			}

			return RedirectToAction(nameof(Edit), new { id = adminEditGameRequest.Id });
		}

		[HttpPost]
		public async Task<IActionResult> Delete(AdminEditGameRequest editGameRequest)
		{
			var deletedGame = await _gameRepository.DeleteAsync(editGameRequest.Id);

			if (deletedGame != null)
			{
				return RedirectToAction(nameof(List));
			}

			return RedirectToAction(nameof(Edit), new { id = editGameRequest.Id });
		}
	}
}
