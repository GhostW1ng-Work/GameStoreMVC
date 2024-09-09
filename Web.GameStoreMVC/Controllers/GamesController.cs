using Microsoft.AspNetCore.Mvc;
using Web.GameStoreMVC.Models.ViewModels;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);
            var gameViewModel = new GameViewModel();

            if(game != null)
            {
                gameViewModel = new GameViewModel
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    ShortDescription = game.ShortDescription,
                    SystemRequirements = game.SystemRequirements,
                    Developer = game.Developer,
                    Price = game.Price,
                    YearOfRelease = game.YearOfRelease,
                    ImageUrl = game.ImageUrl,
                    Languages = game.Languages,
                    Genres = game.Genres,
                    Platforms = game.Platforms
                };
            }
            return View(gameViewModel);
        }
    }
}
