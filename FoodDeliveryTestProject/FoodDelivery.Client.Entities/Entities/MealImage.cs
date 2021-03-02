using FoodDelivery.Common.Interface;

namespace FoodDelivery.Client.Entities
{
	public class MealImage : IIdentifiableEntity
	{
		public int EntityId
		{
			get => MealImageId;
			set => MealImageId = value;
		}
		public int MealImageId { get; set; }
		public byte[] Image { get; set; }
		public int MealId { get; set; }
	}
}
