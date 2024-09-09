using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.GameStoreMVC.Models.ViewModels;

namespace Web.GameStoreMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser
				{
					UserName = registerViewModel.Username,
					Email = registerViewModel.Email
				};

				var identityResult = await _userManager.CreateAsync(user, registerViewModel.Password);

				if (identityResult.Succeeded)
				{
					var roleIdentityResult = await _userManager.AddToRoleAsync(user, "User");

					if (roleIdentityResult.Succeeded)
					{
						return RedirectToAction(nameof(Register));
					}
				}
			}

			return View();
		}

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			var model = new LoginViewModel { ReturnUrl = returnUrl };

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if(ModelState.IsValid)
			{
				var signInResult = await _signInManager
					.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

                if (signInResult.Succeeded && signInResult != null)
                {
					if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
					{
						return Redirect(loginViewModel.ReturnUrl);
					}

					return RedirectToAction("Index", "Home");
                }
            }

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index","Home");
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
