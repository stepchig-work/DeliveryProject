
using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Client.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Client.AngularWeb.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{

		private readonly string cantBlockUser ="Can't block user";

		IAccountsService accountsService;
		public AccountController(IAccountsService accountsService)
		{
			Contract.Requires(accountsService != null);

			this.accountsService = accountsService;

		}
		[HttpPut("[action]")]
		public async Task<IActionResult> BanUser([FromBody] BanUserModel banUserModel)
		{
			try
			{
				await accountsService.BanUser(banUserModel.OwnerId, banUserModel.UserName);
				return Ok();
			}
			catch
			{
				return BadRequest(cantBlockUser);
			}
		}
	}
}
