using System.ComponentModel.DataAnnotations;

namespace Web.GameStoreMVC.Models.ViewModels
{
	public class EditGenreRequest
	{
        public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
    }
}
