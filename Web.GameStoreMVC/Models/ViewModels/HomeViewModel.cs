using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }

    }
}
