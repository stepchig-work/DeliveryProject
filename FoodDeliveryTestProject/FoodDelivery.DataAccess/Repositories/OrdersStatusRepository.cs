using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.DataAccess
{
	public class OrdersStatusRepository : BaseRepository<OrderStatus, FoodDeliveryDbContext>, IOrdersStatusRepository
	{
		public OrdersStatusRepository(IOrderStatusValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{
				
		}

		public IEnumerable<OrderStatus> GetAllStatusesForOrder(int orderId) => dbContext.OrderStatusesSet.Where(os => os.OrderId == orderId);

		protected override OrderStatus FindById(int id, FoodDeliveryDbContext dbContext) => dbContext.OrderStatusesSet.FirstOrDefault(os => os.OrderStatusId == id);
		
	}
}
