using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;

namespace BET.Infrastructure.Repositories
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetAllAsync();
		Task<ActionResultTypes> AddAsync(Product product);
		Task<ActionResultTypes> UpdateAsync(Product product, int productId);
		Task<ActionResultTypes> DeleteAsync(int productId);
	}
}
