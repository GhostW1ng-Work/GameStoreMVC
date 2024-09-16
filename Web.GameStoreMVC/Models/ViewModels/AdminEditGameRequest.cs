using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.GameStoreMVC.Models.ViewModels
{
    public class AdminEditGameRequest
    {
        public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string ShortDescription { get; set; }
		[Required]
		public string SystemRequirements { get; set; }
		[Required]
		public string Developer { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public DateOnly YearOfRelease { get; set; }
		[Required]
		public string ImageUrl {  get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
        public IEnumerable<SelectListItem> Platforms { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
        public string[] SelectedLanguages { get; set; } = Array.Empty<string>();
        public string[] SelectedPlatforms { get; set; } = Array.Empty<string>();
        public string[] SelectedGenres { get; set; } = Array.Empty<string>();
    }
}
