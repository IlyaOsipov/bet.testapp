using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.Repositories;
using BET.Repositories.DataContext;

namespace BET.Repositories.Repos
{
	public class UserRepository : IUserRepository
	{
		private readonly BETDataContext _context;

		public UserRepository(BETDataContext context)
		{
			_context = context;
		}

		public async Task<ActionResultTypes> AddAsync(User user)
		{
			if (await ValidateUserNameAsync(user.FullName).ConfigureAwait(false))
				return ActionResultTypes.UserExists;
			_context.Users.Add(user);
			var result = await _context.SaveChangesAsync().ConfigureAwait(false);
			return ActionResultTypes.Successfully;
		}

		public async Task<ActionResultTypes> DeleteAsync(int userId)
		{
			var targetUser = await _context.Users.FirstOrDefaultAsync(p=>p.UserId == userId)
				.ConfigureAwait(false);
			if (targetUser == null)
				return ActionResultTypes.UserNotExists;
			targetUser.IsDeleted = true;
			targetUser.IsActive = false;
			_context.Users.AddOrUpdate(targetUser);
			var result = await _context.SaveChangesAsync().ConfigureAwait(false);
			return ActionResultTypes.Successfully;
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			return await _context.Users
				.ToListAsync().ConfigureAwait(false);
		}
		
		public async Task<ActionResultTypes> UpdateAsync(User user, int userId)
		{
			var targetUser = await _context.Users.FirstOrDefaultAsync(p => p.UserId == userId)
				.ConfigureAwait(false);
			if (targetUser == null)
				return ActionResultTypes.UserNotExists;

			if (targetUser.FullName != user.FullName.Trim())
			{
				if (await ValidateUserNameAsync(user.FullName).ConfigureAwait(false))
				{
					return ActionResultTypes.UserExists;
				}
			}

			targetUser.FullName = user.FullName;
			targetUser.IsAdmin = user.IsAdmin;
			targetUser.IsDeleted = user.IsDeleted;
			targetUser.IsActive = user.IsActive;

			_context.Users.AddOrUpdate(targetUser);
			var result = await _context.SaveChangesAsync().ConfigureAwait(false);
			return ActionResultTypes.Successfully;
		}

		public async Task<ActionResultTypes> CheckUserExistAsync(string login, string password)
		{
			var targetUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password)
				.ConfigureAwait(false);
			if (targetUser == null)
				return ActionResultTypes.UserNotExists;

			if (!targetUser.IsActive)
				return ActionResultTypes.UserNotActive;

			return ActionResultTypes.Successfully;
		}

		public async Task<User> GetUserAsync(string login, string password)
		{
			var targetUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password)
				.ConfigureAwait(false);
			return targetUser;
		}

		private async Task<bool> ValidateUserNameAsync(string userName)
		{
			return await _context.Users.AnyAsync(v => v.FullName == userName)
				.ConfigureAwait(false);
		}
	}
}
