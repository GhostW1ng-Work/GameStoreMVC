﻿using Web.GameStoreMVC.Models.Domain;

namespace Web.GameStoreMVC.Repositories
{
	public interface IPlatformRepository
	{
		Task<IEnumerable<Platform>> GetAllAsync(int pageNumber = 1, int pageSize = 3);
		Task<Platform?> GetAsync(Guid id);
		Task<Platform> AddAsync(Platform platform);
		Task<Platform?> UpdateAsync(Platform platform);
		Task<Platform?> DeleteAsync(Guid id);
		Task<int> GetCountAsync();
	}
}
