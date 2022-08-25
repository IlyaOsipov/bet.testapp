using AutoMapper;
using BET.Infrastructure.Models;
using BET.Infrastructure.RequestModels;

namespace BET.Web.Api
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<User, UserDO>();
			CreateMap<ProductProfileRO, Product>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id)); 
			CreateMap<UpdateUserProfileRO, User>()
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id)); 
			CreateMap<UserProfileRO, User>()
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id)); 
			CreateMap<Product, ProductDO>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
			CreateMap<CartItem, CartItemDO>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CartItemId));
			CreateMap<Cart, CartDO>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CartId));
		}
	}
}
