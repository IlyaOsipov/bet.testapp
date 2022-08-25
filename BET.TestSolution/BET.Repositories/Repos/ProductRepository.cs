using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.Repositories;
using BET.Repositories.DataContext;

namespace BET.Repositories.Repos
{
	public class ProductRepository : IProductRepository
	{
		private readonly BETDataContext _context;

		public ProductRepository(BETDataContext context)
		{
			_context = context;
		}

		public async Task<ActionResultTypes> AddAsync(Product product)
		{
			if (string.IsNullOrEmpty(product.Name))
				return ActionResultTypes.ProductNameNotExists;
			if (await ValidateModifiedUserNotExistsAsync(product.LastModifiedUserId).ConfigureAwait(false))
				return ActionResultTypes.UserNotExists;			
			if (await ValidateProductAsync(product.Name).ConfigureAwait(false))
				return ActionResultTypes.ProductAlreadyExists;
			product.LastModifiedDateTime = DateTime.Now;
			_context.Products.Add(product);
			var result = await _context.SaveChangesAsync().ConfigureAwait(false);
			return ActionResultTypes.Successfully;
		}

		public async Task<ActionResultTypes> DeleteAsync(int productId)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p=>p.ProductId == productId)
				.ConfigureAwait(false);
			if (product == null)
				return ActionResultTypes.ProductNotExists;
			_context.Products.Remove(product);
			var result = await _context.SaveChangesAsync().ConfigureAwait(false);
			return ActionResultTypes.Successfully;
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _context.Products.ToListAsync().ConfigureAwait(false);
		}
				
		public async Task<ActionResultTypes> UpdateAsync(Product product, int productId)
		{
			if (string.IsNullOrEmpty(product.Name))
				return ActionResultTypes.ProductNameNotExists;
			if (await ValidateModifiedUserNotExistsAsync(product.LastModifiedUserId).ConfigureAwait(false))
				return ActionResultTypes.UserNotExists;
			var targetProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId)
				.ConfigureAwait(false);
			if (targetProduct == null)
				return ActionResultTypes.ProductNotExists;

			if (targetProduct.Name != product.Name.Trim())
			{
				if (await ValidateProductAsync(product.Name).ConfigureAwait(false))
					return ActionResultTypes.ProductAlreadyExists;
			}

			targetProduct.Image = product.Image;
			targetProduct.LastModifiedDateTime = DateTime.Now;
			targetProduct.LastModifiedUserId = product.LastModifiedUserId;
			targetProduct.Name = product.Name;
			targetProduct.Quantity = product.Quantity;
			targetProduct.Price = product.Price;

			_context.Products.AddOrUpdate(targetProduct);
			var result = await _context.SaveChangesAsync().ConfigureAwait(false);
			return ActionResultTypes.Successfully;
		}

		private async Task<bool> ValidateProductAsync(string productName)
		{
			return await _context.Products.AnyAsync(v => v.Name == productName)
				.ConfigureAwait(false);
		}

		private async Task<bool> ValidateModifiedUserNotExistsAsync(int userId)
		{
			return ! await _context.Users.AnyAsync(v => v.UserId == userId && v.IsActive)
				.ConfigureAwait(false);
		}
	}
}
