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
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService; 
		private readonly IMapper _mapper;

		public CartController(ICartService cartService,
			IMapper mapper)
		{
			_cartService = cartService;
			_mapper = mapper;
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Mvc.Route("")]
		[ResponseType(typeof(IEnumerable<CartDO>))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Paged result of carts", typeof(IEnumerable<CartDO>))]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No matching carts found")]
		public async Task<IEnumerable<CartDO>> GetAllCarts()
		{
			return _mapper.Map<IEnumerable<CartDO>>(await _cartService.GetAllAsync());
		}

		[Microsoft.AspNetCore.Mvc.HttpPost]
		[Microsoft.AspNetCore.Mvc.Route("")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Add Cart", typeof(ActionResultData))]
		public async Task<ActionResultData> AddCart([FromBody] CartDO cart)
		{
			var cartModel = _mapper.Map<Cart>(cart);
			return await _cartService.AddAsync(cartModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Mvc.Route("{id:int}")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Get Cart", typeof(CartDO))]
		public async Task<CartDO> GetCart([FromRoute] int id, [FromBody] ProductProfileRO productProfile)
		{
			return _mapper.Map<CartDO>(await _cartService.GetByIdAsync(id));
		}

		[Microsoft.AspNetCore.Mvc.HttpDelete]
		[Microsoft.AspNetCore.Mvc.Route("{id:int}")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Delete Cart", typeof(ActionResultData))]
		public async Task<ActionResultData> DeleteCart([FromRoute] int id)
		{
			return await _cartService.DeleteAsync(id);
		}
	}
}
