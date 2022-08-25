using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;

namespace BET.Infrastructure.Repositories
{
	public interface ICartRepository
	{
		Task<IEnumerable<Cart>> GetAllAsync();
		Task<Cart> GetByIdAsync(int cartId);
		Task<ActionResultTypes> AddAsync(Cart cart);
		Task<ActionResultTypes> DeleteAsync(int cartId);
	}
}
