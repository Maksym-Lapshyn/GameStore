using Common.Entities;
using PaymentService.Repositories;
using System;
using System.Collections.Generic;

namespace PaymentService.Initializers
{
	public class AccountsRepositoryInitializer
	{
		public static void Initialize()
		{
			var visa = new Account
			{
				Balance = 1000,
				CardNumber = 4024007160378997,
				CvvCode = 123,
				ExpirationDate = DateTime.UtcNow.AddMonths(12),
				Owner = new User
				{
					Email = "johndoe@gmail.com",
					FirstName = "John",
					LastName = "Doe"
				}
			};

			var masterCard = new Account
			{
				Balance = 50,
				CardNumber = 5401323513514067,
				CvvCode = 321,
				ExpirationDate = DateTime.UtcNow.AddMonths(12),
				Owner = new User
				{
					Email = "jackblack@gmail.com",
					FirstName = "Jack",
					LastName = "Black"
				}
			};

			AccountsRepository.Accounts.AddRange(new List<Account> { visa, masterCard });
		}
	}
}