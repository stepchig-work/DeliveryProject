
using FoodDelivery.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Business.Interface.Services
{
	public interface IAccountsService
	{
		public Task BanUser(int ownerId, string userName);
		public IEnumerable<Restaurant> GetAllAvailableRestaurantsForUser(int accountId);
		public bool IsUserAllowedForRestaureant(int accountId, int restaurant);
	}
}
