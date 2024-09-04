using Microsoft.AspNetCore.Mvc;
using Web.GameStoreMVC.Models.Domain;
using Web.GameStoreMVC.Models.ViewModels;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
	public class AdminGamesController : Controller
	{
		private readonly IGameRepository _gameRepository;

        public AdminGamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

		[HttpGet]
        public IActionResult Add()
		{
			return View();
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

			await _gameRepository.AddAsync(game);
			return RedirectToAction(nameof(Add));
		}
	}
}
