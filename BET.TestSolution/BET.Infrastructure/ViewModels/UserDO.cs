namespace BET.Infrastructure.Models
{
	public class UserDO
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string Login { get; set; }
	}
}
