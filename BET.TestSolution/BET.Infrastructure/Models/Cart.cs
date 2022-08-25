using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BET.Infrastructure.Models
{
	public class Cart
	{
		[Key]
		public int CartId { get; set; }
		public decimal Totals { get; set; }
		public int LastModifiedUserId { get; set; }
		[ForeignKey("LastModifiedUserId")]
		public virtual User LastModifiedUser { get; set; }
		public DateTime LastModifiedDateTime { get; set; }

		public ICollection<CartItem> CartItems { get; set; }
	}
}
