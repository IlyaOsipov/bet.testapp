using System;
using System.Collections.Generic;

namespace BET.Infrastructure.Models
{
	public class CartDO
	{
		public int Id { get; set; }
		public virtual UserDO LastModifiedUser { get; set; }
		public DateTime LastModifiedDateTime { get; set; }
		public List<CartItemDO> Items { get; set; }
		public decimal Totals { get; set; }
	}
}
