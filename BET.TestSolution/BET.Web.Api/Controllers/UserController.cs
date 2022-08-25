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
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService; 
		private readonly IMapper _mapper;

		public UserController(IUserService userService,
			IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Mvc.Route("")]
		[ResponseType(typeof(IEnumerable<UserDO>))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Paged result of users", typeof(IEnumerable<UserDO>))]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No matching users found")]
		public async Task<IEnumerable<UserDO>> GetAllUsers()
		{
			return _mapper.Map<IEnumerable<UserDO>>(await _userService.GetAllAsync());
		}

		[Microsoft.AspNetCore.Mvc.HttpPost]
		[Microsoft.AspNetCore.Mvc.Route("")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Add User", typeof(ActionResultData))]
		public async Task<ActionResultData> AddUser([Microsoft.AspNetCore.Mvc.FromBody] UserProfileRO user)
		{
			var userModel = _mapper.Map<User>(user);
			return await _userService.AddAsync(userModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpPut]
		[Microsoft.AspNetCore.Mvc.Route("{id}")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Update User", typeof(ActionResultData))]
		public async Task<ActionResultData> UpdateUser([FromRoute] int id, [FromBody] UpdateUserProfileRO user)
		{
			var userModel = _mapper.Map<User>(user);
			return await _userService.UpdateAsync(userModel, userModel.UserId);
		}

		[Microsoft.AspNetCore.Mvc.HttpDelete]
		[Microsoft.AspNetCore.Mvc.Route("{id}")]
		[ResponseType(typeof(ActionResultData))]
		[SwaggerResponse((int)HttpStatusCode.OK, "Delete User", typeof(ActionResultData))]
		public async Task<ActionResultData> DeleteUser([FromRoute] int id)
		{
			return await _userService.DeleteAsync(id);
		}
	}
}
