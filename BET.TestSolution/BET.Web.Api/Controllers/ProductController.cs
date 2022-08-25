using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Description;
using AutoMapper;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.RequestModels;
using BET.Infrastructure.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace BET.Web.Api.Controllers
{
	[ApiController]
	[Microsoft.AspNetCore.Mvc.Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService; 
		private readonly IMapper _mapper;

		public ProductController(IProductService productService,
			IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Mvc.Route("")]
		[ResponseType(typeof(IEnumerable<ProductDO>))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Paged result of products", typeof(IEnumerable<ProductDO>))]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No matching products found")]
		public async Task<IEnumerable<ProductDO>> GetAllProducts()
		{
			return _mapper.Map<IEnumerable<ProductDO>>(await _productService.GetAllAsync());
		}

		[Microsoft.AspNetCore.Mvc.HttpPost]
		[Microsoft.AspNetCore.Mvc.Route("")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Add Product", typeof(UserProfileRO))]
		public async Task<ActionResultData> AddProduct([FromBody] ProductProfileRO productProfile)
		{
			var productModel = _mapper.Map<Product>(productProfile);
			return await _productService.AddAsync(productModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpPut]
		[Microsoft.AspNetCore.Mvc.Route("{id:int}")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Update Product", typeof(ActionResultData))]
		public async Task<ActionResultData> UpdateProduct([FromRoute] int id, [FromBody] ProductProfileRO productProfile)
		{
			var productModel = _mapper.Map<Product>(productProfile);
			return await _productService.UpdateAsync(productModel, id);
		}

		[Microsoft.AspNetCore.Mvc.HttpDelete]
		[Microsoft.AspNetCore.Mvc.Route("{id:int}")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Delete Product", typeof(ActionResultData))]
		public async Task<ActionResultData> DeleteProduct([FromRoute] int id)
		{
			return await _productService.DeleteAsync(id);
		}
	}
}
