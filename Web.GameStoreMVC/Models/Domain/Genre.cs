namespace Web.GameStoreMVC.Models.Domain
{
	public class Genre
	{
        public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<Game> Games { get; set; }
    }
}
