using System.Threading.Tasks;
using BET.Infrastructure.Common;

namespace BET.Infrastructure.Services
{
	public interface IAuthorizationService
	{
		Task<AuthorizationResultData> GetUserAsync(string login, string password);
	}
}
