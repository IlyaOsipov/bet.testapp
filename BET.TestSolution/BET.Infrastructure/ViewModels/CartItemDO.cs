namespace BET.Infrastructure.Models
{
	public class CartItemDO
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string Image { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Totals { get; set; }
	}
}
