using Common.Entities;
using PaymentService.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PaymentService.DAL.Concrete
{
	public class AccountsRepository : IAccountsRepository
	{
		private static readonly List<Account> Accounts = new List<Account>();

		static AccountsRepository()
		{
			Initialize();
		}

		public Account GetSingle(Expression<Func<Account, bool>> predicate)
		{
			return Accounts.First(predicate.Compile());
		}

		public IEnumerable<Account> GetAll(Expression<Func<Account, bool>> predicate = null)
		{
			return predicate == null ? Accounts : Accounts.Where(predicate.Compile());
		}

		public void Insert(Account account)
		{
			Accounts.Add(account);
		}

		public void Delete(int accountId)
		{
			Accounts.First(a => a.Id == accountId).IsDeleted = true;
		}

		public bool Contains(Expression<Func<Account, bool>> predicate)
		{
			return Accounts.Any(predicate.Compile());
		}

		private static void Initialize()
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

			Accounts.AddRange(new List<Account> { visa, masterCard });
		}
	}
}