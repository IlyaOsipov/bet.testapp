namespace BET.Infrastructure.RequestModels
{
	/// <summary>
	/// User Profile
	/// </summary>
	public class UserProfileRO: UserRO
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
