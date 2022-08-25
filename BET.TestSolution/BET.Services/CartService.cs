using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.Repositories;
using BET.Infrastructure.Services;

namespace BET.Services
{
	public class CartService : ICartService
	{
		private readonly ICartRepository _cartRepository;
		public CartService(ICartRepository cartRepository)
		{
			_cartRepository = cartRepository;
		}

		public async Task<ActionResultData> AddAsync(Cart cart)
		{
			var result = await _cartRepository.AddAsync(cart).ConfigureAwait(false);
			if (result == ActionResultTypes.UserNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.USER_NOT_VALID };
		
			if (result == ActionResultTypes.ProductAlreadyExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.PRODUCT_ALREADY_EXISTS };
			return new ActionResultData { Status = ActionResultTypes.Successfully };
		}

		public async Task<ActionResultData> DeleteAsync(int cartId)
		{
			var result = await _cartRepository.DeleteAsync(cartId)
				.ConfigureAwait(false);
			if (result == ActionResultTypes.CartNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.PRODUCT_NOT_EXISTS };
			return new ActionResultData { Status = ActionResultTypes.Successfully };
		}

		public async Task<IEnumerable<Cart>> GetAllAsync()
		{
			return await _cartRepository.GetAllAsync().ConfigureAwait(false);
		}

		public async Task<Cart> GetByIdAsync(int cartId)
		{
			return await _cartRepository.GetByIdAsync(cartId).ConfigureAwait(false);
		}
	}
}
