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

        public AdminGamesController(IGameRepository gameRepository, IGenreRepository genreRepository)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var genres = await _genreRepository.GetAllAsync();

            var viewModel = new AdminGameAddRequest
            {
                Genres = genres.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
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

            foreach (var genre in adminGameAddRequest.SelectedGenres)
            {
                var selectedGenre = Guid.Parse(genre);
                var existingGenre = await _genreRepository.GetAsync(selectedGenre);

                if(existingGenre != null)
                {
                    selectedGenres.Add(existingGenre);
                }
            }

            game.Genres = selectedGenres;

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
                    SelectedGenres = game.Genres.Select(x => x.Id.ToString()).ToArray()
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

            foreach (var selectedGenre in adminEditGameRequest.SelectedGenres)
            {
                if(Guid.TryParse(selectedGenre, out var genreId))
                {
                    var foundGenre = await _genreRepository.GetAsync(genreId);

                    if(foundGenre != null)
                    {
                        editedGenres.Add(foundGenre);
                    }
                }
            }

            game.Genres = editedGenres;
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
