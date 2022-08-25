using System;

namespace BET.Infrastructure.Common
{
	[Serializable]
	public class ActionResultData
	{
		public ActionResultTypes Status { get; set; }
		public string Message { get; set; }
	}
}
