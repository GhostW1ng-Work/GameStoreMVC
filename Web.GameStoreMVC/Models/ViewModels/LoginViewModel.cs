using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Web.GameStoreMVC.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required]
        public string Username { get; set; }
		[Required]
		[MinLength(1, ErrorMessage = "Пароль должен содержать 6 и более символов")]
		public string Password { get; set; }
		public string? ReturnUrl { get; set; }
    }
}
