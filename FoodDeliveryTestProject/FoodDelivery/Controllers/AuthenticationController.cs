using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Client.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoodDelivery.Client.AngularWeb.Controllers
{


	[Route("api/[controller]")]
	public class AuthenticationController : Controller
	{
		Regex lowercase = new Regex("[a-z]+");
		Regex uppercase = new Regex("[A-Z]+");
		Regex digit = new Regex("(\\d)+");
		Regex symbol = new Regex("(\\W)+");

		private readonly string invalidAccountOrUserError = "Invalid account or password";
		private readonly string accountExistsError = "Account already exists";
		private readonly string weakPasswordError = "Need stronger password";
		IAuthenticationService authenticationService;
		IAccountsRepository accountsRepository;
		public AuthenticationController(IAuthenticationService authenticationService, IAccountsRepository accountsRepository)
		{
			Contract.Requires(authenticationService != null);
			Contract.Requires(accountsRepository != null);
			this.authenticationService = authenticationService;
			this.accountsRepository = accountsRepository;
		}

		[HttpPost("[action]")]
		public IActionResult LogIn([FromBody] LogInModel logInModel)
		{
			var account = accountsRepository.GetAccountByUsername(logInModel.Username);
			if (account == null)
			{
				return BadRequest(invalidAccountOrUserError);
			}
			var accountsPassword = accountsRepository.GetAccountHashedPassword(account.UserName);
			var isPasswordValid = authenticationService.ComparePasswords(logInModel.Password, accountsPassword);
			if (!isPasswordValid)
			{
				return BadRequest(invalidAccountOrUserError);
			}

			var returnAccount = new
			{
				account.EntityId,
				account.Name,
				account.Surname,
				account.Address,
				account.UserName,
				account.Role
			};

			return Ok(returnAccount);
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> SignUp([FromBody] SignInModel signinModel)
		{
			if (accountsRepository.IsAccountExists(signinModel.Account.UserName))
			{
				return BadRequest(accountExistsError);
			}

			if (!HasValidPassword(signinModel.Password))
			{
				return BadRequest(weakPasswordError);
			}
			var addedAccount = new Account();
			try
			{
				addedAccount = await accountsRepository.AddAccount(signinModel.Account, signinModel.Password);
				
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			var returnAccount = new
			{
				addedAccount.EntityId,
				addedAccount.Name,
				addedAccount.Surname,
				addedAccount.Address,
				addedAccount.UserName,
				addedAccount.Role
			};

			return Ok(returnAccount);
		}
		private bool HasValidPassword(string pw) => pw.Length >= 7 
													&& lowercase.IsMatch(pw) 
													&& uppercase.IsMatch(pw) 
													&& digit.IsMatch(pw) 
													&& symbol.IsMatch(pw);
		
	}
}
