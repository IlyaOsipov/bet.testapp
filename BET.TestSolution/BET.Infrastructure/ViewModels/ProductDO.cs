namespace BET.Infrastructure.Models
{
	public class ProductDO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public string ProductCategory { get; set; }
		public int ProductCategoryId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
