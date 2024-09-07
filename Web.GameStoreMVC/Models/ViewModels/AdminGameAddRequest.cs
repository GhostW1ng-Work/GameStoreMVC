using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.GameStoreMVC.Models.ViewModels
{
	public class AdminGameAddRequest
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string SystemRequirements { get; set; }
		public string Developer { get; set; }
		public decimal Price { get; set; }
		public DateOnly YearOfRelease { get; set; }
		public IEnumerable<SelectListItem> Languages { get; set; }
		public IEnumerable<SelectListItem> Platforms { get; set; }
		public IEnumerable<SelectListItem> Genres { get; set; }
		public string[] SelectedLanguages { get; set; } = Array.Empty<string>();
		public string[] SelectedPlatforms { get; set; } = Array.Empty<string>();
		public string[] SelectedGenres { get; set; } = Array.Empty<string>();
	}
}
