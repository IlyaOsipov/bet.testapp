using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;

namespace BET.Infrastructure.Services
{
	public interface ICartService
	{
		Task<IEnumerable<Cart>> GetAllAsync();
		Task<Cart> GetByIdAsync(int cartId);
		Task<ActionResultData> AddAsync(Cart cart);
		Task<ActionResultData> DeleteAsync(int cartId);
	}
}
