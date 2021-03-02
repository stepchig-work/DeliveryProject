
namespace FoodDelivery.Client.Entities
{
	public class OrderStatusChangeModel
	{
		public OrderStatuses OrderStatus { get; set; }
		public int OrderId { get; set; }
		public int AccountId { get; set; }
	}
}