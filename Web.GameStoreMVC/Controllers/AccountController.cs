using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Web.GameStoreMVC.Models.ViewModels;

namespace Web.GameStoreMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly ILogger<AccountController> _logger;

		public AccountController(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			IEmailSender emailSender,
			ILogger<AccountController> logger)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_logger = logger;

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
					string email = user.Email;

					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var callbackUrl = Url.Action(
			  nameof(ConfirmEmail),
			  "Account",
			  new { userId = user.Id, code = code },
			  protocol: Request.Scheme
		  );

					await _emailSender.SendEmailAsync(
						email,
						"Подтверждение вашей учетной записи",
						$"Пожалуйста, подтвердите вашу учетную запись, кликнув <a href='{callbackUrl}'>здесь</a>.") ;

					var roleIdentityResult = await _userManager.AddToRoleAsync(user, "User");

					if (roleIdentityResult.Succeeded)
					{
						ViewBag.Message = "Проверьте вашу почту для подтверждения вашей учетной записи.";
						return View("RegistrationConfirmation");
					}
				}
				foreach (var error in identityResult.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

			}

			return View(registerViewModel);
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
			if (ModelState.IsValid)
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
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> ConfirmEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return RedirectToAction(nameof(Index), "Home"); // Или другая страница
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var result = await _userManager.ConfirmEmailAsync(user, code);
				if (result.Succeeded)
				{
					return View(nameof(ConfirmEmail));
				}
			}
			return View();
		}

		[HttpGet]
		public IActionResult RegistrationConfirmation()
		{
			return View();
		}
	}
}
