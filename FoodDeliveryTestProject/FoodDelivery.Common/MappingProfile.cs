using AutoMapper;

using BusinessAccount = FoodDelivery.Business.Entities.Account;
using BusinessMeal = FoodDelivery.Business.Entities.Meal;
using BusinessMealForOrder = FoodDelivery.Business.Entities.MealForOrder;
using BusinessMealImage = FoodDelivery.Business.Entities.MealImage;
using BusinessOrder = FoodDelivery.Business.Entities.Order;
using BusinessOrderStatus = FoodDelivery.Business.Entities.OrderStatus;
using BusinessRestaurant = FoodDelivery.Business.Entities.Restaurant;
using BusinessRestaurantImage = FoodDelivery.Business.Entities.RestaurantImage;
using BusinessBlockedUsers = FoodDelivery.Business.Entities.BlockedUsers;

using ClientAccount = FoodDelivery.Client.Entities.Account;
using ClientMeal = FoodDelivery.Client.Entities.Meal;
using ClientMealForOrder = FoodDelivery.Client.Entities.MealForOrder;
using ClientMealImage = FoodDelivery.Client.Entities.MealImage;
using ClientOrder = FoodDelivery.Client.Entities.Order;
using ClientOrderStatus = FoodDelivery.Client.Entities.OrderStatus;
using ClientRestaurant = FoodDelivery.Client.Entities.Restaurant;
using ClientRestaurantImage = FoodDelivery.Client.Entities.RestaurantImage;
using ClientBlockedUsers = FoodDelivery.Client.Entities.BlockedUsers;

namespace FoodDelivery.Common
{
	public class MappingProfile: Profile
	{
		public MappingProfile()
		{
				

			CreateMap<ClientAccount, BusinessAccount>()
				.MaxDepth(2); ;
			CreateMap<BusinessAccount, ClientAccount>()
				.MaxDepth(2);
			CreateMap<BusinessAccount, BusinessAccount>()
				.MaxDepth(2); ;

			CreateMap<ClientMeal, BusinessMeal>()
				.MaxDepth(2);
			CreateMap<BusinessMeal, ClientMeal>()
				.MaxDepth(2);
			CreateMap<BusinessMeal, BusinessMeal>()
				.MaxDepth(2); 

			CreateMap<ClientMealForOrder, BusinessMealForOrder>()
				.MaxDepth(2); 
			CreateMap<BusinessMealForOrder, ClientMealForOrder>()
				.MaxDepth(2); 
			CreateMap<BusinessMealForOrder, BusinessMealForOrder>()
				.MaxDepth(2); 

			CreateMap<ClientMealImage, BusinessMealImage>()
				.MaxDepth(2); 
			CreateMap<BusinessMealImage, ClientMealImage>()
				.MaxDepth(2); 
			CreateMap<BusinessMealImage, BusinessMealImage>()
				.MaxDepth(2); 

			CreateMap<ClientOrder, BusinessOrder>()
				.MaxDepth(3);
			CreateMap<BusinessOrder, ClientOrder>()
				.MaxDepth(3); 
			CreateMap<BusinessOrder, BusinessOrder>();

			CreateMap<ClientOrderStatus, BusinessOrderStatus>()
				.MaxDepth(2); ;
			CreateMap<BusinessOrderStatus, ClientOrderStatus>()
				.MaxDepth(2);
			CreateMap<BusinessOrderStatus, BusinessOrderStatus>();

			CreateMap<ClientRestaurant, BusinessRestaurant>()
				.MaxDepth(2);
			CreateMap<BusinessRestaurant, ClientRestaurant>()
				.MaxDepth(2); 
			CreateMap<BusinessRestaurant, BusinessRestaurant>()
				.MaxDepth(2); 

			CreateMap<ClientRestaurantImage, BusinessRestaurantImage>()
				.MaxDepth(2); 
			CreateMap<BusinessRestaurantImage, ClientRestaurantImage>()
				.MaxDepth(2); 
			CreateMap<BusinessRestaurantImage, BusinessRestaurantImage>()
				.MaxDepth(2); 

			CreateMap<ClientBlockedUsers, BusinessBlockedUsers>()
				.MaxDepth(2); 
			CreateMap<BusinessBlockedUsers, ClientBlockedUsers>()
				.MaxDepth(2); 
			CreateMap<BusinessBlockedUsers, ClientBlockedUsers>()
				.MaxDepth(2); 
		}

	}
}
