using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;

namespace BET.Infrastructure.Services
{
	public interface IUserService
	{
		Task<IEnumerable<User>> GetAllAsync(); 
		Task<ActionResultData> AddAsync(User user);
		Task<ActionResultData> UpdateAsync(User user, int userId);
		Task<ActionResultData> DeleteAsync(int userId);
	}
}
