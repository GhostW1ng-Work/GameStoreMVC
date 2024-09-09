using System.ComponentModel.DataAnnotations;

namespace Web.GameStoreMVC.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		[MinLength(6, ErrorMessage ="Пароль должен содержать 6 и более символов")]
		public string Password { get; set; }
	}
}
