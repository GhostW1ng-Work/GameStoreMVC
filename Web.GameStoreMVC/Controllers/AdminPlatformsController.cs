using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.GameStoreMVC.Models.Domain;
using Web.GameStoreMVC.Models.ViewModels;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminPlatformsController : Controller
	{
		private readonly IPlatformRepository _platformRepository;

		public AdminPlatformsController(IPlatformRepository platformRepository)
		{
			_platformRepository = platformRepository;
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var platforms = await _platformRepository.GetAllAsync();
			return View(platforms);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddPlatformRequest addPlatformRequest)
		{
			if (ModelState.IsValid)
			{
				var platform = new Platform
				{
					Name = addPlatformRequest.Name
				};

				await _platformRepository.AddAsync(platform);
				return RedirectToAction(nameof(List));
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var platform = await _platformRepository.GetAsync(id);

			if (platform != null)
			{
				var editPlatformRequest = new EditPlatformRequest
				{
					Id = platform.Id,
					Name = platform.Name,
				};
				return View(editPlatformRequest);
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditPlatformRequest editPlatformRequest)
		{
			var platform = new Platform
			{
				Id = editPlatformRequest.Id,
				Name = editPlatformRequest.Name,
			};
			var updatedPlatform = await _platformRepository.UpdateAsync(platform);

			return RedirectToAction(nameof(List), new { Id = platform.Id });
		}

		public async Task<IActionResult> Delete(EditPlatformRequest editPlatformRequest)
		{
			var existingPlatform = await _platformRepository.DeleteAsync(editPlatformRequest.Id);

			if (existingPlatform != null)
			{
				return RedirectToAction(nameof(List));

			}

			return RedirectToAction(nameof(List), new { Id = editPlatformRequest.Id });
		}
	}
}
