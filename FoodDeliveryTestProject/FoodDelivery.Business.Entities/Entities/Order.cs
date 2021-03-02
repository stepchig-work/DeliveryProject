using System;
using System.Collections.Generic;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Entities
{
	public class Order : IIdentifiableEntity
	{
		public int EntityId
		{
			get => OrderId;
			set => OrderId = value;
		}
		public int OrderId { get; set; }
		public DateTime CreationDate { get; set; }
		public Account Customer { get; set; }
		public int CustomerId { get; set; }
		public int RestaurantId { get; set; }
		public List<OrderStatus> OrderStatuses { get; set; }
		public decimal Price { get; set; }
		public List<MealForOrder> MealsForOrder { get; set; }
		public OrderStatuses LatestOrderStatus { get; set; }
		public string RestaurantName { get; set; }
	}
}
