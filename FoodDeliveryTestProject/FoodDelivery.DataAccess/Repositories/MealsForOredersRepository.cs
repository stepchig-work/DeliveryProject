using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.DataAccess
{
	public class MealsForOredersRepository : BaseRepository<MealForOrder, FoodDeliveryDbContext>, IMealsForOrdersRepository
	{
		public MealsForOredersRepository(IMealForOrderValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{

		}

		public IEnumerable<MealForOrder> GetMealsForOrder(int orderId) => dbContext.MealsForOrdersSet.Where(mfo => mfo.OrderId == orderId);


		protected override MealForOrder FindById(int id, FoodDeliveryDbContext dbContext) => dbContext.MealsForOrdersSet.FirstOrDefault(mfo => mfo.MealForOrderId == id);

	}
}
