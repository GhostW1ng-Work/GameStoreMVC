using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.GameStoreMVC.Models;
using Web.GameStoreMVC.Models.ViewModels;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IGameRepository _gameRepository;
		private readonly IGenreRepository _genreRepository;
		private readonly ILanguageRepository _languageRepository;
		private readonly IPlatformRepository _platformRepository;

		public HomeController(ILogger<HomeController> logger,
			IGameRepository gameRepository,
			IGenreRepository genreRepository,
			ILanguageRepository languageRepository,
			IPlatformRepository platformRepository)
		{
			_logger = logger;
			_gameRepository = gameRepository;
			_genreRepository = genreRepository;
			_languageRepository = languageRepository;
			_platformRepository = platformRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Index(
			string? searchQuery,
			string? sortBy,
			string? sortDirection,
			int pageSize = 3,
			int pageNumber = 1)
		{
			var totalRecords = await _gameRepository.GetCountAsync();
			var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

			if(pageNumber > totalPages)
			{
				pageNumber--;
			}

			if (pageNumber < 1)
			{
				pageNumber++;
			}

			ViewBag.SearchQuery = searchQuery;
			ViewBag.SortBy = sortBy;
			ViewBag.SortDirection = sortDirection;
			ViewBag.TotalPages = totalPages;
			ViewBag.PageSize = pageSize;
			ViewBag.PageNumber = pageNumber;


			var games = await _gameRepository.GetAllAsync(searchQuery, sortBy, sortDirection,
				pageSize,pageNumber);

			var genres = await _genreRepository.GetAllAsync();
			var languages = await _languageRepository.GetAllAsync();
			var platforms = await _platformRepository.GetAllAsync();

			var homeViewModel = new HomeViewModel
			{
				Games = games,
				Genres = genres,
				Languages = languages,
				Platforms = platforms
			};

			return View(homeViewModel);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
