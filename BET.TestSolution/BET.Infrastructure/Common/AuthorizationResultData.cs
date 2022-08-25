using System;
using BET.Infrastructure.Models;

namespace BET.Infrastructure.Common
{
	[Serializable]
	public class AuthorizationResultData : ActionResultData
	{
		public UserModel User { get; set; }
	}
}
