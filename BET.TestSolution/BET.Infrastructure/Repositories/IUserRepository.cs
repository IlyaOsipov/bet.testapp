using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;

namespace BET.Infrastructure.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAllAsync(); 
		Task<ActionResultTypes> AddAsync(User user);
		Task<ActionResultTypes> UpdateAsync(User user, int userId);
		Task<ActionResultTypes> DeleteAsync(int userId);
		Task<User> GetUserAsync(string login, string password);
		Task<ActionResultTypes> CheckUserExistAsync(string login, string password);
	}
}
