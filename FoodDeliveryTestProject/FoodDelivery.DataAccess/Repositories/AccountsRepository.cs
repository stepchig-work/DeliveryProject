using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Linq;

namespace FoodDelivery.DataAccess
{
	public class AccountsRepository : BaseRepository<Account, FoodDeliveryDbContext>, IAccountsRepository
	{
		public AccountsRepository(IAccountValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{}

		public Account GetAccountByUsername(string username)
		{
			return dbContext.AccountsSet.FirstOrDefault(a => a.UserName == username);
		}

		public bool IsAccountExists(string username)
		{
			return dbContext.AccountsSet.Any(a => a.UserName == username);
		}

		protected override Account FindById(int id, FoodDeliveryDbContext dbContext)
		{
			return dbContext.AccountsSet.FirstOrDefault(a => a.AccountId == id);	
		}
	}
}
