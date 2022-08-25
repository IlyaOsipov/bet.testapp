using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BET.Infrastructure.Models
{
	public class CartItem
	{
		[Key]
		public int CartItemId { get; set; }
		public int CartId { get; set; }
		[ForeignKey("CartId")]
		public virtual Cart Cart { get; set; }
		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public decimal Totals { get; set; }
	}
}
