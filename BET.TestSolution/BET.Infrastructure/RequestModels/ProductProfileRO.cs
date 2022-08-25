namespace BET.Infrastructure.RequestModels
{
	/// <summary>
	/// Product Profile Request Object
	/// </summary>
	public class ProductProfileRO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int LastModifiedUserId { get; set; }
	}
}
