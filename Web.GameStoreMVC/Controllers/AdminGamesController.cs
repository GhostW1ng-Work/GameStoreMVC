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
			
			if(game!= null)
			{
				var editedGame = new AdminEditGameRequest
				{
					Id = game.Id,
					Name = game.Name,
					Description = game.Description,
					SystemRequirements = game.SystemRequirements,
					Developer = game.Developer,
					Price = game.Price,
					YearOfRelease = game.YearOfRelease
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

			var updatedGame = await _gameRepository.UpdateAsync(game);

			if(updatedGame != null)
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

			return RedirectToAction(nameof(Edit), new {id =  editGameRequest.Id});
        }
	}
}
