using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;

namespace BET.Infrastructure.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetAllAsync();
		Task<ActionResultData> AddAsync(Product product);
		Task<ActionResultData> UpdateAsync(Product product, int productId);
		Task<ActionResultData> DeleteAsync(int productId);
	}
}
