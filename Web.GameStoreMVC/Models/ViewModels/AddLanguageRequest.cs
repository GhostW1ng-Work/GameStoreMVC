using System.ComponentModel.DataAnnotations;

namespace Web.GameStoreMVC.Models.ViewModels
{
    public class AddLanguageRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
