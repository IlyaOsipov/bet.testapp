using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BET.Infrastructure.Models
{
	public class User
	{
		[Key]
		public int UserId { get; set; }
		public string Login { get; set; }
		public string FullName { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string Password { get; set; }
	}
}
