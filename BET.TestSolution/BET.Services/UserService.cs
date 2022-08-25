using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.Repositories;
using BET.Infrastructure.Services;

namespace BET.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<ActionResultData> AddAsync(User user)
		{
			var result = await _userRepository.AddAsync(user).ConfigureAwait(false);
			if (result == ActionResultTypes.UserExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.USER_ALREADY_EXISTS };
			return new ActionResultData { Status = ActionResultTypes.Successfully };
		}

		public async Task<ActionResultData> DeleteAsync(int userId)
		{
			var result = await _userRepository.DeleteAsync( userId).ConfigureAwait(false);
			if (result == ActionResultTypes.UserNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.USER_NOT_EXISTS };
			return new ActionResultData { Status = ActionResultTypes.Successfully };
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			return await _userRepository.GetAllAsync().ConfigureAwait(false);
		}
		
		public async Task<ActionResultData> UpdateAsync(User user, int userId)
		{
			var result = await _userRepository.UpdateAsync(user, userId).ConfigureAwait(false);

			if (result == ActionResultTypes.UserNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.USER_NOT_EXISTS };

			if (result == ActionResultTypes.UserExists)
			{
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.USER_ALREADY_EXISTS };
			}
			return new ActionResultData { Status = ActionResultTypes.Successfully };
		}
	}
}
