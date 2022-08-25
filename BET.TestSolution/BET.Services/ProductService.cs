using System.Collections.Generic;
using System.Threading.Tasks;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.Repositories;
using BET.Infrastructure.Services;

namespace BET.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<ActionResultData> AddAsync(Product product)
		{
			var result = await _productRepository.AddAsync(product).ConfigureAwait(false);
			if (result == ActionResultTypes.ProductNameNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.PRODUCT_NAME_NOT_EXISTS };
			if (result == ActionResultTypes.UserNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.USER_NOT_VALID };
		
			if (result == ActionResultTypes.ProductAlreadyExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.PRODUCT_ALREADY_EXISTS };
			return new ActionResultData { Status = ActionResultTypes.Successfully };
		}

		public async Task<ActionResultData> DeleteAsync(int productId)
		{
			var result = await _productRepository.DeleteAsync(productId)
				.ConfigureAwait(false);
			if (result == ActionResultTypes.ProductNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.PRODUCT_NOT_EXISTS };
			return new ActionResultData { Status = ActionResultTypes.Successfully };
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _productRepository.GetAllAsync().ConfigureAwait(false);
		}

		public async Task<ActionResultData> UpdateAsync(Product product, int productId)
		{
			var result = await _productRepository.UpdateAsync(product, productId)
				.ConfigureAwait(false);
			if (result == ActionResultTypes.ProductNotExists)
				return new ActionResultData {Status = ActionResultTypes.Fails, Message = Messages.PRODUCT_NOT_EXISTS};
			if (result == ActionResultTypes.ProductNameNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.PRODUCT_NAME_NOT_EXISTS };
			if (result == ActionResultTypes.UserNotExists)
				return new ActionResultData { Status = ActionResultTypes.Fails, Message = Messages.USER_NOT_VALID };
		
			if (result == ActionResultTypes.ProductAlreadyExists)
				return new ActionResultData
					{Status = ActionResultTypes.Fails, Message = Messages.PRODUCT_ALREADY_EXISTS};

			return new ActionResultData {Status = ActionResultTypes.Successfully};
		}
	}
}
