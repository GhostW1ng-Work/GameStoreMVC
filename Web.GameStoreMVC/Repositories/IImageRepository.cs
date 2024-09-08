namespace Web.GameStoreMVC.Repositories
{
	public interface IImageRepository
	{
		Task<string> UploadAsync(IFormFile file);
	}
}
