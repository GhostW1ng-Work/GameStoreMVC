using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.GameStoreMVC.Models.ViewModels;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
	[Authorize(Roles ="SuperAdmin")]
	public class AdminUsersController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
			_userRepository = userRepository;
            _userManager = userManager;
        }

		[HttpGet]
		public async Task<IActionResult> List(int pageNumber = 1, int pageSize = 3)
		{
			var totalRecords = await _userRepository.GetCountAsync() - 1;
			var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

			if (pageNumber > totalPages)
			{
				pageNumber--;
			}

			if (pageNumber < 1)
			{
				pageNumber++;
			}

			ViewBag.TotalPages = totalPages;
			ViewBag.PageSize = pageSize;
			ViewBag.PageNumber = pageNumber;

			var users = await _userRepository.GetAllAsync(pageNumber:pageNumber, pageSize:pageSize);

			var userViewModel = new UserViewModel();
			userViewModel.Users = new List<User>();

			foreach (var user in users)
			{
				userViewModel.Users.Add(new User
				{
					Id = Guid.Parse(user.Id),
					Username = user.UserName,
					Email = user.Email
				});
			}
		
			return View(userViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> List(UserViewModel userViewModel)
		{
			var identityUser = new IdentityUser
			{
				UserName = userViewModel.UserName,
				Email = userViewModel.Email
			};

			var identityResult = await _userManager.CreateAsync(identityUser, userViewModel.Password);

			if (identityResult!=null) 
			{
				if(identityResult.Succeeded)
				{
					var roles = new List<string> { "User" };
					if (userViewModel.IsAdminRole)
						roles.Add("Admin");

					identityResult = await _userManager.AddToRolesAsync(identityUser, roles);

					if(identityResult.Succeeded && identityResult != null)
					{
						return RedirectToAction(nameof(List), "AdminUsers");
					}
				}
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());

			if (user != null)
			{
				var identityResult = await _userManager.DeleteAsync(user);

				if (identityResult!=null && identityResult.Succeeded)
				{
					return RedirectToAction(nameof(List), "AdminUsers");
				}
			}

			return View();
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
