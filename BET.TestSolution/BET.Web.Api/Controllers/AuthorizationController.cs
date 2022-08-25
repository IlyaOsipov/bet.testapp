using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Description;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.RequestModels;
using BET.Infrastructure.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace BET.Web.Api.Controllers
{
	[ApiController]
	[Microsoft.AspNetCore.Mvc.Route("[controller]")]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthorizationService _authorizationService;

		public AuthenticationController(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
		}

		[Microsoft.AspNetCore.Mvc.HttpPost]
		[Microsoft.AspNetCore.Mvc.Route("auth")]
		[ResponseType(typeof(UserModel))]
		[SwaggerResponse((int)HttpStatusCode.OK, "User Authenticate and Authorize", typeof(UserModel))]
		public async Task<AuthorizationResultData> Auth([Microsoft.AspNetCore.Mvc.FromBody] UserRO user)
		{
			return await _authorizationService.GetUserAsync(user.Login, user.Password);
		}
    }
}
