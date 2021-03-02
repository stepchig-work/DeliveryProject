using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Client.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Business.Services
{
	public class AccountsService : IAccountsService
	{
		IRestaurantsRepository restaurantsRepository;
		IBlockedUsersRepository blockedUsersRepository;
		IAccountsRepository accountsRepository;
		IOrdersRepository ordersRepository;
		public AccountsService(IBlockedUsersRepository blockedUsersRepository,
			IRestaurantsRepository restaurantsRepository,
			IAccountsRepository accountsRepository,
			IOrdersRepository ordersRepository)
		{
			Contract.Requires(restaurantsRepository != null);
			Contract.Requires(blockedUsersRepository != null);
			Contract.Requires(accountsRepository != null);
			Contract.Requires(ordersRepository != null);

			this.restaurantsRepository = restaurantsRepository;
			this.blockedUsersRepository = blockedUsersRepository;
			this.accountsRepository = accountsRepository;
			this.ordersRepository = ordersRepository;
		}

		public async Task BanUser(int ownerId, string userName)
		{
			var user = accountsRepository.GetAccountByUsername(userName);

			if (user == null)
			{
				throw new Exception();
			}

			var blockedUser = new BlockedUsers()
			{
				AccountId = user.AccountId,
				OwnerId = ownerId
			};

			await blockedUsersRepository.AddAsync(blockedUser);
		}

		public IEnumerable<Restaurant> GetAllAvailableRestaurantsForUser(int accountId)
		{
			var blokedUserOwners = blockedUsersRepository.BlokedUserOwners(accountId);
			var result = restaurantsRepository.GetEntities().Where(r => !blokedUserOwners.Any(buw => buw.OwnerId == r.OwnerId));
			return result;
		}

		public bool IsUserAllowedForRestaureant(int accountId, int restaurantId)
		{
			if (accountId == 0)
			{
				return true;
			}

			var account = accountsRepository.Find(accountId);

			if (account.Role == AccountRoles.RegularUser)
			{
				var blokedUserOwners = blockedUsersRepository.BlokedUserOwners(accountId);
				var restaurant = restaurantsRepository.Find(restaurantId);
				return !blokedUserOwners.Any(buo => buo.OwnerId == restaurant.OwnerId);
			}
			else
			{
				var restarurants = restaurantsRepository.GetOwnersRestaurants(accountId);
				return restarurants.Any(r => r.EntityId == restaurantId);
			}
		}
		public bool IsUserAllowedForOrder(int accountId, int orderId)
		{
			if (accountId == 0)
			{
				return false;
			}

			var order = ordersRepository.Find(orderId);
			var account = accountsRepository.Find(accountId);

			if (account.Role == AccountRoles.RegularUser)
			{
				return order.CustomerId == accountId;
			}
			else
			{
				var restarurants = restaurantsRepository.GetOwnersRestaurants(accountId);
				return restarurants.Any(r => r.EntityId == order.RestaurantId);
			}
		}
	}
}
