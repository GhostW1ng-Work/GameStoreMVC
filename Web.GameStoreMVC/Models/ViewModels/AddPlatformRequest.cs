using System.ComponentModel.DataAnnotations;

namespace Web.GameStoreMVC.Models.ViewModels
{
	public class AddPlatformRequest
	{
		[Required]
		public string Name { get; set; }
	}
}
