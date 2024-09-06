using System.ComponentModel.DataAnnotations;

namespace Web.GameStoreMVC.Models.ViewModels
{
	public class AddGenreRequest
	{
		[Required]
		public string Name { get; set; }
	}
}
