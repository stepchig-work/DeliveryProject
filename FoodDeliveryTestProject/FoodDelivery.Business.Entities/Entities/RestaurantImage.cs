using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Entities
{
	public class RestaurantImage : IIdentifiableEntity
	{
		public int EntityId
		{
			get => RestaurantImageId;
			set => RestaurantImageId = value;
		}
		public int RestaurantImageId { get; set; }

		public byte[] Image { get; set; }
		public Restaurant Restaurant { get; set; }
		public int RestaurantId { get; set; }
	}
}
