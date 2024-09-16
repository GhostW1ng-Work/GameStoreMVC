using System.ComponentModel.DataAnnotations;

namespace Web.GameStoreMVC.Models.ViewModels
{
    public class EditLanguageRequest
    {
        public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
    }
}
