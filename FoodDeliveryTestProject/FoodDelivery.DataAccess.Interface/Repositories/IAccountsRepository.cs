using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IAccountsRepository : IRepository<Account>
	{
		public Account GetAccountByUsername(string username);

		public bool IsAccountExists(string username);
	}
}
