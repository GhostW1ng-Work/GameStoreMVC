namespace Web.GameStoreMVC.Models.Domain
{
	public class Game
	{
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string SystemRequirements { get; set; }
		public string Developer { get; set; }
		public decimal Price { get; set; }
		public DateOnly YearOfRelease { get; set; }
		public string ImageUrl { get; set; }
		public ICollection<Language> Languages { get; set; }
		public ICollection<Platform> Platforms { get; set; }
		public ICollection<Genre> Genres { get; set; }
	}
}
