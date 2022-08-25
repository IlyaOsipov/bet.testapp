using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.Repositories;
using BET.Infrastructure.Services;

namespace BET.Services
{
	public class AuthorizationService : IAuthorizationService
	{
		private readonly IUserRepository _userRepository;
		public AuthorizationService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public async Task<AuthorizationResultData> GetUserAsync(string login, string password)
		{
			var userExists = await _userRepository.CheckUserExistAsync(login, password).ConfigureAwait(false);
			
			if(userExists != ActionResultTypes.Successfully)
				return new AuthorizationResultData { Status = ActionResultTypes.Fails, Message = Messages.USER_NOT_EXISTS };

			var targetUser = await _userRepository.GetUserAsync(login, password).ConfigureAwait(false);
			
			return new AuthorizationResultData
			{
				Status = ActionResultTypes.Successfully, User = new UserModel
				{
					FullName = targetUser.FullName,
					IsAdmin = targetUser.IsAdmin,
					Id = targetUser.UserId
				}
			};
		}
	}
}
