using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BET.Infrastructure.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string Image { get; set; }
		public int LastModifiedUserId { get; set; }
		[ForeignKey("LastModifiedUserId")]
		public virtual User LastModifiedUser { get; set; }
		public DateTime LastModifiedDateTime { get; set; }
	}
}
