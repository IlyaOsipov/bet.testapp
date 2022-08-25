using System.Threading.Tasks;
using BET.Infrastructure.Common;

namespace BET.Infrastructure.Repositories
{
	public interface IAuthorizationRepository
	{
		Task<AuthorizationResultData> GetUserAsync(string login, string password);
	}
}
