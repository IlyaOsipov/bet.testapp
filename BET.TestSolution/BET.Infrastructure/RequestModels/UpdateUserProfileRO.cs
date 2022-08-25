namespace BET.Infrastructure.RequestModels
{
	/// <summary>
	/// User Request Object
	/// </summary>
	public class UpdateUserProfileRO
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
