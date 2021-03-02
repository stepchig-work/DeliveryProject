using FoodDelivery.Common.Interface;
using System;

namespace FoodDelivery.Business.Entities
{
	public class OrderStatus : IIdentifiableEntity
	{
		public int EntityId
		{
			get => OrderStatusId;
			set => OrderStatusId = value;
		}
		public int OrderStatusId { get; set; }
		public OrderStatuses Status { get; set; }
		public DateTime StatusChangeTime { get; set; }
		public Order Order { get; set; }
		public int OrderId { get; set; }
	}
}
