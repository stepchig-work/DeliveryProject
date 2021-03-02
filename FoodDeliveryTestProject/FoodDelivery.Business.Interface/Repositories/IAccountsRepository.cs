using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;
using System.Threading.Tasks;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IAccountsRepository : IRepository<Account>
	{
		public Account GetAccountByUsername(string username);
		public bool IsAccountExists(string username);
		public Task<Account> AddAccount(Account account, string password);
		public string GetAccountHashedPassword(string username);
	}
}
