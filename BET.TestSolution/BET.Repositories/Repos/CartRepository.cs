using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.Repositories;
using BET.Repositories.DataContext;

namespace BET.Repositories.Repos
{
	public class CartRepository : ICartRepository
	{
		private readonly BETDataContext _context;

		public CartRepository(BETDataContext context)
		{
			_context = context;
		}

		public async Task<ActionResultTypes> AddAsync(Cart cart)
		{
			if (cart.CartItems.Count == 0)
				return ActionResultTypes.CartIsEmpty;
			if (await ValidateModifiedUserNotExistsAsync(cart.LastModifiedUserId).ConfigureAwait(false))
				return ActionResultTypes.UserNotExists;
			var carItemErrors = new List<int>();
			cart.CartItems.ToList().ForEach(async ci =>
			{
				if (await ValidateProductQuantityAsync(ci.Product.Name, ci.Quantity).ConfigureAwait(false))
					carItemErrors.Add(ci.ProductId);
			});
			if (carItemErrors.Count > 0)
				return ActionResultTypes.ProductQuantityError;

			cart.LastModifiedDateTime = DateTime.Now;
			_context.Carts.Add(cart);
			_context.CartItems.AddRange(cart.CartItems);
			var result = await _context.SaveChangesAsync().ConfigureAwait(false);
			return ActionResultTypes.Successfully;
		}

		public async Task<ActionResultTypes> DeleteAsync(int cartId)
		{
			var cart = await _context.Carts.FirstOrDefaultAsync(p=>p.CartId == cartId)
				.ConfigureAwait(false);
			if (cart == null)
				return ActionResultTypes.CartNotExists;
			_context.Carts.Remove(cart);
			var result = await _context.SaveChangesAsync().ConfigureAwait(false);
			return ActionResultTypes.Successfully;
		}

		public async Task<IEnumerable<Cart>> GetAllAsync()
		{
			return await _context.Carts.ToListAsync().ConfigureAwait(false);
		}
				
		public async Task<Cart> GetByIdAsync(int cartId)
		{
			return await _context.Carts.FirstOrDefaultAsync(v => v.CartId == cartId)
				.ConfigureAwait(false);
		}

		private async Task<bool> ValidateProductQuantityAsync(string productName, int count)
		{
			return await _context.Products.AnyAsync(v => v.Name == productName && v.Quantity >= count)
				.ConfigureAwait(false);
		}

		private async Task<bool> ValidateModifiedUserNotExistsAsync(int userId)
		{
			return ! await _context.Users.AnyAsync(v => v.UserId == userId && v.IsActive)
				.ConfigureAwait(false);
		}
	}
}
